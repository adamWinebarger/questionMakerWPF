   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages&Question_Maker_Pro_WPF_Prototype.Pages  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      >    8Question_Maker_Pro_WPF_Prototype.Pages.ReportDataVizPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5        N� Title$  ReportDataVizPage��5
      .��5      � ���#e9ecef � RowDefinitions 5   
   �� 5      $��0.25* �6   �� 5      $��0.75* �6   .+�5   
   �� $��Report Data View��6   $��0��6+   $��Center=�68   $��Center=�6U   $��5q�6p   $
��22pt(�6{   $
��Bold"�6�   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��0.55* �6#   �� 5      $
��0.4* �6#   .+�5      �� $��0��6   $��5q�6)   .+�5      �� -   5         ��Name$ questionLabel��5      $��Banana��63   $��Center=�6A   $��5q�6^   $
��Wrap=�6i   $
��12pt(�6}     LiveCharts.Wpf.PieChart  5      -   5          Name$ dataVizPieChart��5      &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�68      	Hoverable$
 True��6P   $	��150��6a   $��20q�6n   5      �� 5        �� Orientation$ 
Horizontal=�6   $��0��66   $��Center=�6F   $��5q�6c   .+�5!      �� -   5         ��Name$ 
backButton��5!      $��Previous��61   $	��5 0q�6D   $
��15 5q�6Q   $��85��6`   6k   �� 5"      -   5        $ 
nextButton��5"      $
��Next��61   $	��5 0q�6@   $
��15 5q�6M   $��85��6\   6g   5#      �� 5$      $��Question Select:��6   $��	20 5 25 0q�63   �� 5%      -   5        	 ��Name$	 questionComboBox��5%      $
��25 0q�65   5'      5(      �� 5*      
�������$��1��6,   $��1��69   $	�� 10q�6I   $��2q�6V   ._�5+      �� $��1��6   $��10q�6-   
 �� CanVerticallyScroll$

 True��69   .+�5,      �� $��Filter Reports By:��6   $
��Left=�69   $��12(�6T   $
��Bold"�6b   �� 5.      $��Date Range:��6   $
��Left=�62   $��5 5 5 0q�6M   �� 5/      -   5        $	 dateComboBox��5/      $��	15 0 25 0q�67   6J   �� 52      -   5         ��Name$ customDateRangeStackPanel��52   !   $ 
Horizontal=�6D   $��20 10q�6]   .+�53      �� -   5        $ fromStackPanel��53   %   $ Vertical=�6=   $��0 0 25 0q�6T   .+�54      �� $��From:��6$   (  "System.Windows.Controls.DatePicker  55      -	   5          Name$ fromDateBox��55   )   $	��100��6>   56      �� 57      -
   5        $ toStackPanel��57   %   $ Vertical=�6;   $��25 0 0 0q�6R   .+�58      �� $	��To:��6$     59      -   5        $ 	toDateBox��59   )   $	��100��6<   5:      5;      �� 5=      $��Time of Day:��6   $
��Left=�63   $��5 5 0 0q�6N   �� 5>      -   5        $	 timeOfDayComboBox��5>      $��
15 0 25 10q�6:   �� 5@      $��Parent of Teacher:��6   $
��Left=�69   $��5 5 0 0q�6T   �� 5A      -   5        $	 parentOrTeacherComboBox��5A      $��
15 0 25 10q�6@   �� 5C      -   5        $ searchButton��5C      $��Search��63   $��Center=�6D   $��	5 10 5 15q�6v   $
��10 2q�6�   $	��125��6�   �� 5D      -   5        $ return2DataViewButton��5D      $��Return to Dataview��6<   $��Center=�6Y   $��5q�6�   $
��10 2q�6�   $	��125��6�   5E      5F      5G      5I      5J      