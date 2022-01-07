from fastapi import FastAPI, Depends, HTTPException, status
from fastapi.middleware.cors import CORSMiddleware
from fastapi.security import HTTPBasic, HTTPBasicCredentials
from fastapi import Body
from decouple import config
from typing import List
import uvicorn




app = FastAPI()
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

username = config("UpdateUserName")
password = config("UpdatePassword")
security = HTTPBasic()
async def authorize(credentials: HTTPBasicCredentials):
    if credentials.username == username and credentials.password == password:
        return True
    else:
        return False

versions:List[str] = ["v3.0.0"]

# For adding updates
@app.post("/add_update")
async def add_update(req:str = Body(...), credentials: HTTPBasicCredentials = Depends(security)):
    if await authorize(credentials):
        versions.append(req)
        return {"Error":False, "ErrorMessage":""}
    else:
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="You are not authorized to add updates",
            headers={"WWW-Authenticate": "Basic"}
        )


# For requesting for updates
@app.get("/check_update/{req}")
async def check_update(req:str):
    try:
        if versions.index(req) != versions[-1]:
            return {"NewUpdate":True, "UpdateVersion":versions[-1]}
        else:
            return {"NewUpdate":False, "UpdateVersion":versions[-1]}
    except ValueError as e:
        print(e)
        return {"NewUpdate":True, "UpdateVersion":versions[-1]}



if __name__ == "__main__":
    uvicorn.run("update_API:app", port=7676, host='0.0.0.0', reload=True)