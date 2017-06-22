using LCL.Domain.Entities;
using System;
using System.Collections.Generic;
namespace LCL.Domain.Services
{
    public interface ILocalizationService
    {
        void DeleteLocaleStringResource(LocaleStringResource localeStringResource);
        string GetResource(string resourceKey);
        List<LocaleStringResource> GetStringResourceAll();
        void InsertLocaleStringResource(LocaleStringResource localeStringResource);
        void UpdateLocaleStringResource(LocaleStringResource localeStringResource);
    }
}
