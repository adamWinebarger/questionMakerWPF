   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Question_Maker_Pro_WPF_Prototype Question_Maker_Pro_WPF_Prototype  5   	   1    +Question_Maker_Pro_WPF_Prototype.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:Question_Maker_Pro_WPF_Prototype     5   	     � Title$  
MainWindow��5   	   $	��600��6   $
��1000��6)   $	��600��5	   	   $	��800��6   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��0 �6   �� 5      .+�5   
   �� ���#e9ecef.+�5      �� $��NavBar��6   $��Center=�6%   $��10, 25q�6B   �� 5      -   5         ��Name$ 
homeButton��5      $
��Home��6*   $��Center=�69   $��10, 35q�6V   $
��Auto��6g   $
��Auto��6t   $��5,5,5,5q�6�   6�   �� 5      -   5        $ newTesterButton��5      $��Create Child��6.   $��Center=�6E   $��10, 35q�6b   $
��Auto��6r   $
��Auto��6   $��5,5,5,5q�6�   6�   �� 5      -   5        $ viewTestingDataButton��5      $��View Child Data��64   $��Center=�6N   $��10, 35q�6k   $
��Auto��6|   $
��Auto��6�   $��5,5,5,5q�6�   6�   �� 5      -   5        $ preloadQuestionsButton��5      $��Prebuild Questions List��65   $��Center=�6W   $��10, 35q�6t   $
��Auto��6�   $
��Auto��6�   $��5,5,5,5q�6�   6�   �� 5      -   5        $ 
testButton��5      $��Button��6)   $��Center=�6:   $��10, 35q�6W   $
��Auto��6h   $
��Auto��6u   $��5,5,5,5q�6�   6�   5       � 5   
   -   5        $��navframe��5       $��1��6     � NavigationUIVisibility$ Hidden=�60   ���#e9ecef6e   5      5      