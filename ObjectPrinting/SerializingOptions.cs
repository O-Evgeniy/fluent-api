﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectPrinting
{
    public class SerializingOptions
    {
        private readonly List<Type> excludedTypes = new List<Type>();
        private readonly List<MemberInfo> excludedMembers = new List<MemberInfo>();
        private readonly Dictionary<MemberInfo, Func<object, string>> rules =
            new Dictionary<MemberInfo, Func<object, string>>();

        public bool AllowCyclicReferences { get; set; }

        public bool TryGetRule(MemberInfo memberInfo, out Func<object, string> rule)
        {
            return rules.TryGetValue(memberInfo, out rule);
        }
        
        public void Exclude(Type type)
        {
            excludedTypes.Add(type);
        }
        
        public bool IsExcluded(Type type)
        {
            return excludedTypes.Contains(type);
        }

        public void Exclude(MemberInfo memberInfo)
        {
            excludedMembers.Add(memberInfo);
        }

        public bool IsExcluded(MemberInfo memberInfo)
        {
            return excludedMembers.Contains(memberInfo);
        }
        
        public void AddSerializationRule<TType>(Func<TType, string> rule)
        {
            rules.Add(typeof(TType),obj => rule((TType)obj));
        }

        public void AddSerializationRule<TType>(MemberInfo memberInfo, Func<TType, string> rule)
        {
            rules.Add(memberInfo,obj => rule((TType)obj));
        }
    }
}