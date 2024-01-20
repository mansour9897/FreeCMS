using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeCMS.DomainModels.System
{
    public class ViewObject
    {
        public ViewObject(){}
        public ViewObject(Guid visitorId, string ip,string path,string query,string browser,string os,
            string referrer)
        {
            this.Id = Guid.NewGuid();
            this.VisitorId = visitorId;
            this.Ip = ip;
            this.Path = path;
            this.Query = query;
            this.Date = DateTime.Now;
            this.Browser = browser;
            this.OS = os;
            this.Referrer = referrer;
        }
        public Guid Id { get; set; }
        public Guid VisitorId { get; set; }
        public string Ip { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public DateTime Date { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public string Referrer { get; set; }
    }
}