using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Lesson_1.Enums
{
	/* ---------------------------------------------------------------------------------------
	 *								C# NOTES: 'ENUMERATIONS'
	 * ---------------------------------------------------------------------------------------
	 * An 'Enumeration' (or 'enum' for short) is a special kind of value type declaration which
	 * defines a set of one or more named constants which are associated with an integer value.
	 * Enums are useful in situations where you want to categorize complex state (anything more
	 * complex than a binary choice or boolean) in a way that is meaningful and easy to understand
	 * for other developers.
	 * 
	 * By default, enum values are implicitly assigned integer values in the order in which they 
	 * are defined, starting with 0 and incrementing by 1 for each subsequent value.  This can be
	 * viewed by hovering the cursor over one of the defined values.  The integers associated with 
	 * each constant can also be explicitly defined (as shown below) which can be useful in cases
	 * where the integer has some meaning in respect to the constant (for example, HTTP status codes)
	 * OR in cases where the enum represents a 'bit flag'.  
	 * 
	 * If an integer is explicitly defined for one constant, implicit assignment of subsequent constants
	 * will increment from the last explicit value defined.
	 * 
	 * Typically, when working with enums, only the constant value is used.  However, in situations
	 * where the integer value is necessary, it can be coerced by 'casting' the enum value to an integer
	 * (and vice-versa).
	 * 
	 * To learn more about enums and casting, see:
	 *	https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
	 *	https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions
	 * ---------------------------------------------------------------------------------------
	 */
	public enum PrimaryColor
	{
		None = -1,
		Red,
		Green,
		Blue
	}
}
