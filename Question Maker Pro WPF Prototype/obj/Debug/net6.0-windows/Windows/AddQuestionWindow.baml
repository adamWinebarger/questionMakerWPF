   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Question_Maker_Pro_WPF_Prototype.Windows(Question_Maker_Pro_WPF_Prototype.Windows  5   	   @    :Question_Maker_Pro_WPF_Prototype.Windows.AddQuestionWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hlocal6clr-namespace:Question_Maker_Pro_WPF_Prototype.Windows     5   	     � Title$  AddQuestionWindow��5   	   $	��300��6#   $	��650��60   
�������.��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.2* �6   �� 5      $
��0.5* �6   �� 5      $
��0.3* �6   .+�5   
   �� -   5         ��Name$ 
titleLabel��5      $��Add New Question��6$   $��Center=�6?   $��Center=�6\   $��0��6w   $��22(�6�   $
��Bold"�6�   $��10q�6�   �� 5   
   -   5         ��Name$ questionTextBlock��5      $��1��6-   $
��Left=�6:   $��Center=�6U   $��38, 26q�6p   
�������$	��574��6�   $��90��6�   � 5   
   $��2��6   $
��10 0q�6    � ColumnDefinitions 5      �� 5      $
��0.6* �6#   �� 5      $
��0.4* �6#   .+�5      � -   5         �Name$ addQuestionGrid��5       5      �� 5      $
��0.3* �6$   �� 5      $
��0.6* �6$   .+�5       �� $��Select Questionnaire��6   $��0��67   $��Center=�6D   $��Center=�6a   $��0q�6|   A� 5!      $��1��6    A� Columns$ 3��6+   $��Center=�67   $	��Top=�6T   $	�� 10q�6l   .+�5"      	� -   5         	�Name$ parentRadioButton��5"   "   $��Parent��6=   $
��15 0q�6N   	� 5#      -   5        $ teacherRadioButton��5#   "   $��Teacher��6>   $�� 0q�6P   	� 5$      -   5        $ bothRadioButton��5$   "   $
��Both��6;   $��0q�6J   5%      5&      A� 5(      $��1��6   $ 2��6*   $��30,30,30,30q�66   .+�5)      �� -   5         ��Name$ submitButton��5)      $��Submit��6/   $
��15 0q�6@   6N   �� 5*      -   5        $ 
backButton��5*      $
��Back��6-   $
��15 0q�6<   6J   5+      5,      5.      5/      