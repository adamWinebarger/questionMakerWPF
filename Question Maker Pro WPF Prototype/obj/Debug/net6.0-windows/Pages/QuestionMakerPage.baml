   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages&Question_Maker_Pro_WPF_Prototype.Pages  5      >    8Question_Maker_Pro_WPF_Prototype.Pages.QuestionMakerPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages     5        N� Title$  QuestionMakerPage��5	      .��5      � ���#e9ecef � RowDefinitions 5   
   �� 5      $��0.25* �6   �� 5      .+�5   
   �� -   5         ��Name$ 
titleLabel��5      $��1��6$   $��Input Questions��64   $��Center=�6N   $��Center=�6k   $
��36pt(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ questionTextBlock��5      
�������$��Center=�6@   $��1��6]    �� TextWrapping$
 Wrap=�6j   $�� ��6~   $	��Top=�6�   $	��160��6�   $	��668��6�   $��0,70,0,0q�6�   �� 5   
   -   5        $ questionNumberLabel��5      $��Input question #1:��6-   $
��12pt(�6J   $
��Left=�6Z   $��	66,37,0,0q�6u   $��1��6�   $	��Top=�6�   �� 5   
   -   5         ��Name$ currentQuestionComboBox��5      $
��Left=�64   $��
647,26,0,0q�6O   $��1��6c   $	��Top=�6p   $	��120��6�   $
��12pt(�6�   �� 5   
   $��Jump to Question #��6   $
��Left=�6-   $��
618,85,0,0q�6H   $	��Top=�6\   $
��12pt(�6t   $��2��6�   � 5   
   $��400,252,66,27q�6   $��1��6&   .+�5      �� -   5         ��Name$ addQuestionButton��5      $��Add Question��60   $��Right=�6G   $��2q�6c   $	��Top=�6o   $��	0,0,255,0q�6�   �� 5      -   5        $ submitButton��5      $��Finish��6+   $��Right=�6<   $��2q�6X   $��65��6d   $	��Top=�6r   $��	0,47,10,0q�6�   �� 5      -   5        $ insertButton��5      $��Insert��6+   $��Right=�6<   $��2q�6X   $	��Top=�6d   $��65��6|   $��	154,0,0,0q�6�   �� 5      -   5        $ replaceButton��5      $��Replace��6,   $��Right=�6>   $��2q�6Z   $	��Top=�6f   $��65��6~   $��0,0,85,0q�6�   �� 5      -	   5        $ removeButton��5      $��Remove��6+   $	��Top=�6=   $��2q�6U   $��Right=�6w   $��65��6�   $��	0,0,167,0q�6�   �� 5!      -
   5        $ 
nextButton��5!      $
��Next��6)   $��2q�68   $	��Top=�6D   $��Center=�6\   $��Right=�6�   $��65��6�   $��
0,47,269,0q�6�   �� 5"      -   5        $ 
saveButton��5"      $
��Save��6)   $��2q�69   $��65��6E   $	��Top=�6S   $��Right=�6k   $��
0,47,102,0q�6�   �� 5#      -   5        $ 
backButton��5#      $
��Back��6)   $��2q�68   $��65��6Z   $��21��6e   $	��Top=�6q   $��Right=�6�   $��84,47,185,0q�6�   5%      �� 5&   
   $��Test Type Selection: ��6   $
��Left=�60   $��
66,252,0,0q�6K   $��1��6_   $	��Top=�6l   $
��12pt(�6�   	� 5'   
   -   5         	�Name$ parentRadioButton��5'      $��Parent��61   $
��Left=�6B   $��
66,286,0,0q�6]   $��1��6q   $	��Top=�6~   $
��12pt(�6�   6�   	� 5(   
   -   5        $ teacherRadioButton��5(       d� 	IsChecked$
 TrueY�62   $��Teacher��6C   $
��Left=�6U   $��152,286,0,0q�6p   $��1��6�   $	��Top=�6�   $
��12pt(�6�   6�   5+      5,      