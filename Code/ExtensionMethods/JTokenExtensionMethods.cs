using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace watchtower.Code.ExtensionMethods {

    public static class JTokenExtensionMethods {

        public static string GetString(this JToken ext, string name, string def) {
            return ext.Value<string?>(name) ?? def;
        }

        public static string? NullableString(this JToken ext, string name) {
            return ext.Value<string?>(name);
        }

        public static DateTime CensusTimestamp(this JToken ext, string field) {
            return DateTimeOffset.FromUnixTimeSeconds(ext.Value<int?>(field) ?? 0).UtcDateTime;
        }

    }
}
