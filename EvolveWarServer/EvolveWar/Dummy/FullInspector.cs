using System;

namespace FullInspector
{
    public class BaseBehavior
    {

    }

    public class InspectorCommentAttribute : Attribute
	{
		public InspectorCommentAttribute(string s)
		{

		}
	}
	public class InspectorName : Attribute
	{
		public InspectorName(string s)
		{

		}
	}
	public class InspectorShowIf : Attribute
	{
		public InspectorShowIf(string s)
		{

		}
	}

	public class InspectorDisabled : Attribute
	{
		public InspectorDisabled()
		{

		}
	}
	public class HideInInspector : Attribute
	{
		public HideInInspector()
		{

		}
	}
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class InspectorRangeAttribute : Attribute
	{
		/// <summary>
		/// The minimum value.
		/// </summary>
		public float Min;

		/// <summary>
		/// The maximum value.
		/// </summary>
		public float Max;

		/// <summary>
		/// The step to use. This is optional.
		/// </summary>
		public float Step = float.NaN;

		public InspectorRangeAttribute(float min, float max)
		{
			Min = min;
			Max = max;
		}
	}
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class InspectorCollectionShowItemDropdownAttribute : Attribute
	{
		public bool IsCollapsedByDefault = true;
	}
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class InspectorMarginAttribute : Attribute
	{
		public int Margin;
		public InspectorMarginAttribute(int margin)
		{
			Margin = margin;
		}		
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class InspectorCollapsedFoldoutAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class InspectorDivider : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class InspectorCollectionPagerAttribute : Attribute
	{
		/// <summary>
		/// The minimum collection length before the pager is displayed. A value of 0 means that the pager
		/// will always be displayed, and a negative value means that the pager will never be displayed. Use
		/// AlwaysHide or AlwaysShow as utility methods for setting PageMinimumCollectionLength to those
		/// special values.
		/// </summary>
		public int PageMinimumCollectionLength;
		
		public InspectorCollectionPagerAttribute()
		{
		}

		public InspectorCollectionPagerAttribute(int pageMinimumCollectionLength)
		{
			PageMinimumCollectionLength = pageMinimumCollectionLength;
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
	public sealed class InspectorDropdownNameAttribute : Attribute
	{
		/// <summary>
		/// The name that the type will use in the abstract type dropdown selection
		/// wizard. The default value is the C# formatted type name for the type. You
		/// can create "folders" within the dropdown popup by using '/'; for example,
		/// "folder/my type" will appear inside "folder" as "my type".
		/// </summary>
		public string DisplayName;

		/// <summary>
		/// Sets the name of the type to use in the abstract type dropdown.
		/// </summary>
		/// <param name="displayName">
		/// The name that the type will use in the abstract type dropdown selection
		/// wizard. The default value is the C# formatted type name for the type. You
		/// can create "folders" within the dropdown popup by using '/'; for example,
		/// "folder/my type" will appear inside "folder" as "my type".
		/// </param>
		public InspectorDropdownNameAttribute(string displayName)
		{
			DisplayName = displayName;
		}
	}

	public sealed class InspectorDatabaseEditorAttribute : Attribute
	{
	}

	public class InspectorTooltipAttribute : Attribute
	{
		public InspectorTooltipAttribute(string name)
		{

		}
	}

	public class InspectorHideIf : Attribute
	{
		public InspectorHideIf(string str)
		{

		}
	}
	public class InspectorHidePrimary : Attribute
	{
	}

	public class InspectorTextArea : Attribute
	{

	}
}
