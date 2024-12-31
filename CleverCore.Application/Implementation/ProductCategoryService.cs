﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleverCore.Application.Interfaces;
using CleverCore.Application.ViewModels;
using CleverCore.Data.Entities;
using CleverCore.Data.Enums;
using CleverCore.Data.IRepositories;
using CleverCore.Infrastructure.Interfaces;

namespace CleverCore.Application.Implementation
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            var productCategory = _mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            return productCategoryVm;
        }
        public void Delete(int id)
        {
            _productCategoryRepository.Remove(id);
        }
        public List<ProductCategoryViewModel> GetAll()
        {
            return _productCategoryRepository.FindAll().OrderBy(x => x.ParentId)
                 .ProjectTo<ProductCategoryViewModel>(_mapper.ConfigurationProvider).ToList();
        }
        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productCategoryRepository.FindAll(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.ParentId).ProjectTo<ProductCategoryViewModel>(_mapper.ConfigurationProvider).ToList();
            else
                return _productCategoryRepository.FindAll().OrderBy(x => x.ParentId)
                    .ProjectTo<ProductCategoryViewModel>(_mapper.ConfigurationProvider)
                    .ToList();
        }
        public List<ProductCategoryViewModel> GetAllByParentId(int parentId)
        {
            return _productCategoryRepository.FindAll(x => x.Status == Status.Active
            && x.ParentId == parentId)
             .ProjectTo<ProductCategoryViewModel>(_mapper.ConfigurationProvider)
             .ToList();
        }
        public ProductCategoryViewModel GetById(int id)
        {
            return _mapper.Map<ProductCategory, ProductCategoryViewModel>(_productCategoryRepository.FindById(id));
        }
        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            var query = _productCategoryRepository
                .FindAll(x => x.HomeFlag == true, c => c.Products)
                  .OrderBy(x => x.HomeOrder)
                  .Take(top).ProjectTo<ProductCategoryViewModel>(_mapper.ConfigurationProvider);
            var categories = query.ToList();
            foreach (var category in categories)
            {
                //category.Products = _productRepository
                //    .FindAll(x => x.HotFlag == true && x.CategoryId == category.Id)
                //    .OrderByDescending(x => x.DateCreated)
                //    .Take(5)
                //    .ProjectTo<ProductViewModel>().ToList();
            }
            return categories;
        }
        public void ReOrder(int sourceId, int targetId)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            throw new NotImplementedException();
        }
        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            throw new NotImplementedException();
        }
    }
}
