BASE_URL="https://github.com/Abdesol/CutCode/releases/download/"
VERSION="__VERSION__"
FILE="CutCode.__VERSION__.linux.x64.zip"
FOLDER="CutCode __VERSION__ linux x64"


if [ "$EUID" -ne 0 ]
  then echo "Please run as root"
  exit
fi

#Make cutcode tmp file
rm -rf cutcode-tmp
mkdir cutcode-tmp
cd cutcode-tmp
dialog --infobox "installing CutCode..." 20 60
#Get built files and unzip them
curl -LO $BASE_URL/$VERSION/$FILE
unzip $FILE

#make directory to put built files into and put the files
rm -rf /opt/CutCode/
mkdir /opt/CutCode/
cp -r "$FOLDER"/* /opt/CutCode

#make needed file executables
chmod +x /opt/CutCode/CutCode
#add file to /usr/bin/ and make it executable
echo "/opt/CutCode/CutCode" > /usr/bin/CutCode
chmod +x /usr/bin/CutCode 
#add .desktop file
cp "$FOLDER"/CutCode.desktop /usr/share/applications/

# Delete the temp file
cd ..
rm -rf cutcode-tmp

whiptail --msgbox "CutCode has been installed successfully"



if whiptail --yesno "Do you want to open CutCode?" 20 60 ;then
    CutCode
else
    whiptail --msgbox "Thanks for installing CutCode, you can either launch it by running CutCode or search for CutCode in your app drawer" 20 60
fi
