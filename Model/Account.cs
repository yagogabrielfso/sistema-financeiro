using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Account
    {

        private int? _id;
        private string _description;
        private double _value;
        private char _type;
        private DateTime _dueDate;
        private Category _category;

        public int? Id { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }

        public char Type
        {
            get => _type;
            set => _type = !value.Equals('P') && !value.Equals('R') ? throw new Exception("Use P para pagar e R para receber.") : value;
        }

        public DateTime DueDate { get; set; }
        public Category Category { get; set; }

    }
}
