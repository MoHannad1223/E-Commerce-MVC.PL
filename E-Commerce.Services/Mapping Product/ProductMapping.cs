using AutoMapper;
using E_Commerce.Data.DataOrEntity;
using E_Commerce.Services.Model_View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Services.Mapping_Product
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductVM, Product>()
            .ForMember(dest => dest.PictureUrl, opt => opt.Ignore()) // لأنه يتم رفعه يدويًا
            .ForMember(dest => dest.ProductBrand, opt => opt.Ignore())
            .ForMember(dest => dest.ProductType, opt => opt.Ignore())
            ; // لو بتعالجه يدويًا من Image مثلاً;

            CreateMap<UpdateProductVM, Product>()
                .ForMember(dest => dest.ProductBrand, opt => opt.Ignore())
                .ForMember(dest => dest.ProductType, opt => opt.Ignore())
                .ForMember(dest => dest.PictureUrl, opt => opt.Ignore()); // لو بتعالجه يدويًا من Image مثلاً;

            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.Image, opt => opt.Ignore()) // لا يُعاد من الـ Entity
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name));

        }
    }
}
