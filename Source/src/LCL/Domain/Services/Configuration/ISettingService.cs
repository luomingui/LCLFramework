
using LCL.Config;
using LCL.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Services
{
    public partial interface ISettingService
    {
        Setting GetSettingById(Guid settingId);
        void DeleteSetting(Setting setting);
        T GetSettingByKey<T>(string key, T defaultValue = default(T),
            int storeId = 0, bool loadSharedValueIfNotFound = false);
        void SetSetting<T>(string key, T value, int storeId = 0, bool clearCache = true);
        IList<Setting> GetAllSettings();
        bool SettingExists<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int storeId = 0)
            where T : ISettings, new();
        T LoadSetting<T>(int storeId = 0) where T : ISettings, new();
        void SaveSetting<T>(T settings, int storeId = 0) where T : ISettings, new();
        void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,
            int storeId = 0, bool clearCache = true) where T : ISettings, new();
        void DeleteSetting<T>() where T : ISettings, new();
        void DeleteSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector, int storeId = 0) where T : ISettings, new();
        void ClearCache();
    }
}
