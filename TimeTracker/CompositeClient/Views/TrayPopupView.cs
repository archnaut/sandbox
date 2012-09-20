using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CompositeClient.Views
{
	public partial class TrayPopupView : UserControl, ITrayPopupView
	{
		public TrayPopupView()
		{
			InitializeComponent();
		}
		
		public UIElement ToUIElement()
		{
			return this;
		}
	}
}