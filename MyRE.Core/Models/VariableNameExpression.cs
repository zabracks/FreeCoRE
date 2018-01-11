﻿using System.ComponentModel.DataAnnotations;

namespace MyRE.Core.Models
{
    public class VariableNameExpression : Expression
    {
        [MaxLength(128)]
        public string VariableName { get; set; }
    }
}