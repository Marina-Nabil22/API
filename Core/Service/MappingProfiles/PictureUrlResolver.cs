using AutoMapper;
using DomainLayer.Models;
using Shared.DateTransfrObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Services.MappingProfiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;
        public PictureUrlResolver(IConfiguration configuration) {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
           if(string.IsNullOrEmpty(source.PictureUrl)) {
                return string.Empty;
            }
            else
            {

                // var Url = $"https://localhost:7215/{source.PictureUrl}";
                var Url = $"{_configuration.GetSection("Urls")[""]}{source.PictureUrl}";

                return Url;
            }
          
        }
    }
}
