   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Question_Maker_Pro_WPF_Prototype.Windows(Question_Maker_Pro_WPF_Prototype.Windows  5   	   H    BQuestion_Maker_Pro_WPF_Prototype.Windows.QuestionnaireViewerWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hlocal6clr-namespace:Question_Maker_Pro_WPF_Prototype.Windows     5   	     � Title$  QuestionnaireViewerWindow��5   	   $	��450��6+   $	��400��68   .��5	      �  � RowDefinitions 5
   
   �� 5      $��0.25* �6   �� 5      $
��0.5* �6   �� 5      $��0.25* �6   .+�5   
   �� -   5         ��Name$ reportTitleTextBlock��5      $!��Report for Remmy Remmington��62   $��Center=�6U   $��25q�6r   $
��Wrap=�6~   $
��Bold"�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ questionnaireDataGrid��5      $��1��62   $��Stretch=�6?   $��Stretch=�6]   $��25q�6y      AutoGenerateColumns$ False��5      "   VerticalScrollBarVisibility$
 Auto=�6*    � 	Resources 5      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%+   '        �� 5      "  6   5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Question��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.75* 67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *Key+   ElementStyle 5      �� 5      " ��6    .�5      ��  �� Property � �� �� Value$
 Wrap=�6=   �� 5       �@��Padding$ 2q�68   5      5!        5"      $	 Answer��6%   $

 0.4* 65    6B   �� *Value+5$      �� 5%   
   -   5         ��Name$ okButton��5%      $��Ok��6#   $��2��60   $��Center=�6=   $��Center=�6Z   $��50��6u   $��25q�6�   $	��5 1q�6�   6�   5&      5'      