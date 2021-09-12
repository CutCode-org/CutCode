using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutCode
{
    public class ShareAddRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Passw { get; set; }
    }

    public class ShareAddResponseModel
    {
        public bool Done { get; set; }
        public string? Key { get; set; }
        public string? Message { get; set; }
    }

    public class ShareGetRequestModel
    {
        public string Key { get; set; }
        public string Passw { get; set; }
    }

    public class ShareGetResponseModel
    {
        public bool Done { get; set; }
        public string? Message { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }
    }
}
