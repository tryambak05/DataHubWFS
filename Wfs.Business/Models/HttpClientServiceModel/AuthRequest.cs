﻿namespace Wfs.Business.Models.HttpClientServiceModel
{
    public class AuthRequest
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string audience { get; set; }
        public string grant_type { get; set; }
    }
}