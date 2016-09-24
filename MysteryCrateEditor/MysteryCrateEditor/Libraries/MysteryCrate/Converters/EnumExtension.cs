using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MysteryCrateEditor.Libraries.MysteryCrate.Converters
{
    public class EnumExtension : MarkupExtension
    {
        private Type enumType;
        public Type EnumType
        {
            get { return enumType; }
            set
            {
                if (value != enumType)
                {
                    if (value != null)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Must be enum");
                    }
                    enumType = value;
                }
            }
        }

        public EnumExtension() { }
        public EnumExtension(Type enumType)
        {
            this.EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.enumType == null)
                throw new InvalidOperationException("EnumType must be specified");
            Type actualEnumType = Nullable.GetUnderlyingType(this.enumType) ?? this.enumType;
            Array enumValues = Enum.GetValues(actualEnumType);
            return enumValues;

        }
    }
}
