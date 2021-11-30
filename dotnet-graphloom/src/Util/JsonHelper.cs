//-----------------------------------------------------------------------
// <copyright file="JsonHelper.cs" company="github.com/jiefenn8">
//     Copyright (c) 2019 - GraphLoom contributors
//     (github.com/jiefenn8/dotnet-graphloom)
//
//     This software is made available under the terms of Apache License,
//     Version 2.0.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GraphLoom.Mapper.Util
{
    /// <summary>
    /// This class defines the base methods in handling the serialisation of 
    /// third party and custom types using Newtonsoft JSON for .NET GraphLoom;
    /// </summary>
    public static class JsonHelper
    {

        /// <summary>
        /// Returns collection of all custom <see cref="JsonConverter"/> to be 
        /// loaded.
        /// </summary>
        /// <returns>collection of custom JSON converters</returns>
        private static JsonConverter[] LoadConverters()
        {
            return new JsonConverter[]
            {
                new UniqueIdConverter()
            };
        }

        /// <summary>
        /// Returns a JSON string representation of the given object with custom 
        /// converters loaded to handle any custom object to JSON conversion as
        /// needed. Sets the resulting JSON string formatting to indented before 
        /// returning. 
        /// </summary>
        /// <param name="value">the object to convert to JSON</param>
        /// <returns>the string of the JSON conversion result</returns>
        public static string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented, LoadConverters());
        }

        /// <summary>
        /// This class extends the <see cref="JsonConverter"/> class to define 
        /// custom methods handling the serialisation of specific objects and 
        /// types. 
        /// </summary>
        private class UniqueIdConverter : JsonConverter<IUniqueId>
        {
            /// <inheritdoc/>
            public override IUniqueId ReadJson(JsonReader reader, Type objectType, IUniqueId existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc/>
            public override void WriteJson(JsonWriter writer, IUniqueId value, JsonSerializer serializer)
            {
                JObject jsonObj = new JObject();
                jsonObj.AddFirst(new JProperty("id", value.GetUniqueId()));
                jsonObj.WriteTo(writer);
            }
        }
    }
}
