using Entities.CommonModels.ConfigurationModule;
using Helpers.CommonHelpers.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.ConversionHelpers
{
    public static class UrlHelper
    {
        public static string ReplaceQueryParamsInUrl(string url, Dictionary<string,string> QueryParams)
        {
            try
            {
                //-- replace {langCode} in url
                if (url.Contains("{langCode}") && QueryParams!=null && QueryParams.ContainsKey("langCode"))
                {
                    url = ReplaceLanguageCodeInUrl(url, QueryParams["langCode"]);
                }
                return url;
            }
            catch
            {
                throw;
            }
           
        }

        public static string ReplaceLanguageCodeInUrl(string url, string LangCode)
        {
            url = url.Replace("{langCode}", LangCode);
            return url;
        }

        public static string GetMenuLocalizationName(string MenuMainName ,string? LocalizationJsonData , string languageCode)
        {
            try
            {
                string menu = string.Empty;
                int langId = 0;
                langId = CommonConversionHelper.GetLanguageIdbyLanguageCode(languageCode);
                if (langId == (short)LanguagesEnum.English)//-- In case of English, return the same menu. No need of handling
                {
                    return menu = MenuMainName;
                }

                if (String.IsNullOrWhiteSpace(LocalizationJsonData))
                {
                    return menu = MenuMainName;
                }

                var LocalizationMenuInfo = JsonConvert.DeserializeObject<List<LocalizationMenuInfo>>(LocalizationJsonData);

               
                menu = LocalizationMenuInfo?.Where(x => x.langId == langId).FirstOrDefault()?.text ?? MenuMainName;

                if (String.IsNullOrEmpty(menu))
                {
                    return MenuMainName;
                }
                else
                {
                    return menu;
                }
            }
            catch
            {
                return MenuMainName;
            }
           
          
        }

    }
}
