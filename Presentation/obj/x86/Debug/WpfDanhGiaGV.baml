   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfDanhGiaGV   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$!  Đánh giá sau khóa học��5   	   $��557.373��6'   $��870.732��68    � WindowStartupLocation$  CenterScreen=�6H    � Icon$ Icons/VBSP.ico��6o   .��5      � .+�5   
   �� -   5        $��btnClose��5      $��Đóng��6   $
��Left=�6    $��24��6;   $��757,481,0,0q�6G   $	��Top=�6\   $��84��6t   ��5      �� 5	       �� 
TargetType" ��6   .�5
      ��  �� Property � �� �� Value 5      �� 5       �� 
TargetType" ��6.   .R�5   "   �� $��border��6)   $��1q�67    �� Padding$	 4,2q�6K   
������� �� CornerRadius$ 3��6p   #��z��6�   ._�5   &   �� $��content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5      	 �� Triggers	 5      P� 5      
 P� Property
 �@J�IsMouseOver P� Value . . �5      ��  � ��
 �/���6;   5      5      5      �� 5   
   -   5        $��	btnUpdate��5   ~   $
��Lưu��6   $
��Left=�6   $��24��6:   $��646,481,0,0q�6F   $	��Top=�6[   $��84��6s   ��5      �� 5      " ��6   .�5      ��  � �� 5       �� 5!      " ��6.   .R�5"   "   �� $��border��6)   $��1q�67   $	 4,2q�6K   
�������$ 3��6p   #��z��6�   ._�5#   &   �� $��content��6o   $��Center=�67   $��Center=�6T   6o   5$   #   5%      5'      	 5(      P� 5)      
 �@J�IsMouseOver . . �5*      ��  � ��
 �/���6;   5+      5-      50      �� 51   
   -   5        $��
lblGetData��51   7   $��Lấy mẫu đánh giá��6   $��27��6+   $
��auto��6I   $��226,17,527,482q�6z   ��52      �� 53      " ��6   	 54      P� 55      
 �@J�IsMouseOver . . �56      ��  � ��
 �  ��6C   �� 57       � ��$
 Bold"�6C   58      .�5:      ��  � ��
 � � �6;   �� 5;       � ��$ Comic Sans MS)�6;   5<      5>      (  "System.Windows.Controls.DatePicker  5A   
   -   5          Name$ dtpNgay��5A   P   $��24��6   $
��Left=�6!   $��
108,19,0,0q�6<   $	��Top=�6a   $	��111��6y   �� 5B   
   -   5         ��Name$ lblNgay��5B   _   $��Ngày học��6   $
��auto��6#   $
��Left=�61   $��	25,17,0,0q�6L   $	��Top=�6p   $
��auto��6�   &   System.Windows.Controls.DataGrid  5C   
   -   5        $��dgvData��5C   �      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     	RowHeight$ 30Q�6b   $	��406��6t   $
��Left=�6�   $��	10,49,0,0q�6�   $	��Top=�6�   $	��843��6�      CanUserResizeRows .     Columns 5D      0  *System.Windows.Controls.DataGridTextColumn  5U      ,  &System.Windows.Controls.DataGridColumn   Header$ TT��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6M      
IsReadOnly .    
FontWeight$
 Bold"�6k      
Foreground
 � � �1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   ��  �� Path$ TT�   HeaderStyle 5V      �� 5W      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5X      ��  � ��$ Center=�6K   �� 5Y       � ��$ Center=�6I   5Z         ElementStyle 5\      �� 5]      " ��6    .�5^      ��  �@��TextAlignment$
 Left=�6>   �� 5_       � ��$ Center=�6B   5`      5b        5c      $ Nội dung đánh giá��6%   $	 200 6a    . $
 Bold"�6   
 � � � 6@   �� $ CHITIEU� 5d      �� 5e      "  6    .�5f      ��  � ��$ Center=�6K   5g       5i      �� 5j      " ��6    .�5k      ��  �@��TextAlignment$
 Left=�6>   �� 5l       � ��$ Center=�6B   �� 5m       � {� . 6:   5n      5p      4  .System.Windows.Controls.DataGridCheckBoxColumn  5r         CanUserReorder .     CanUserResize .     CanUserSort .  $ Tốt��6�   $ 70 6�    6)   �� $	 TOT�6�     5s       .   .   .  $
 Khá��6�   $ 70 6�    6)   �� $	 KHA�6�     5t       .   .   .  $ Trung Bình��6�   $ 70 6�    6)   �� $ TB�6�     5v      $ Đề Xuất��6%   $ * 6V    .  $ Normal"�6s    66   �� $ DEXUAT� 5w      �� 5x      "  6    .�5y      ��  � ��$ Center=�6K   5z       5|      �� 5}      " ��6    .�5~      ��  �@��TextAlignment$ Center=�6>   �� 5       � ��$ Center=�6B   �� 5�       � {� . 6:   5�      5�      5�      5�      5�      