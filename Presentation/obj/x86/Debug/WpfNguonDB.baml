   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfNguonDB   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$*  $Nhập  chỉ tiêu nguồn dư nợ��5   	   $	��648��6,   $
��1160��69    � WindowStartupLocation$  CenterScreen=�6F    � Icon$ Icons/VBSP.ico��6�   .��5      � .+�5   
   �� -   5        $��lblNgay��5   [   $��Ngày��6   $��23��6   $
��Left=�6+   $��538,574,0,0q�6F   $	��Top=�6j   $��43��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dtpNgay��5   Q   $��27��6   $
��Left=�6!   $��601,574,0,0q�6<   $	��Top=�6`   $	��104��6x   6�   �� 5   
   -   5        $��btnClose��5   ]   $��Thoát��6   $��27��6!   $
��Left=�6-   $��859,574,0,0q�6H   $	��Top=�6m   $��98��6�   6�   �� 5	   
   -   5        $��	btnUpdate��5	   [   $
��Lưu��6   $��27��6   $
��Left=�6+   $��732,574,0,0q�6F   $	��Top=�6l   $��98��6�   6�   &   System.Windows.Controls.DataGrid  5
   
   -   5        $��dgvData��5
   j      AutoGenerateColumns .  $	��491��6/   $
��Left=�6<   $��	12,53,0,0q�6W   $	��Top=�6y   $
��1114��6�      CanUserAddRows .     CanUserDeleteRows .     AlternatingRowBackground
 �����   RowBackground
 �����   	RowHeight$ 24Q�6
  	   HorizontalGridLinesBrush
	 �����
   VerticalGridLinesBrush

 �����   ColumnHeaderHeight$ 24Q�6c   � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%|   '        �� 5      "  6   .�5      ��  � ��
 �����63   �� 5       � ��$ Center=�6C   5      5         Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      ,  &System.Windows.Controls.DataGridColumn   CanUserReorder .     CanUserResize .     CanUserSort .     Header$ Chọn��6�      Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 6�      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ��  �� Path$
 CHON�6�   0  *System.Windows.Controls.DataGridTextColumn  5      $	 STT��6%   $ 50 6N    .     
FontWeight$
 Bold"�6l      
Foreground
 � � � 62   �� $ TT�   HeaderStyle 5      �� 5      "  6    .�5      ��  � ��$ Center=�6K   5         ElementStyle 5      �� 5      " ��6    .�5      ��  �@��TextAlignment$ Center=�6>   �� 5       � ��$ Center=�6B   �� 5       � {� . 6:   5      5!        5"      $ Chỉ Tiêu��6%   $	 350 6V    . $
 Bold"�6t   
 � � � 67   �� $ TENCT� 5#      �� 5$      "  6    .�5%      ��  � ��$
 Left=�6K   5&       5(      �� 5)      " ��6    .�5*      ��  �@��TextAlignment$
 Left=�6>   �� 5+       � ��$ Center=�6B   �� 5,       � {� . 6:   5-      5/        50      $ Quảng Trạch��6%   $ 80 6W    .  $
 Bold"�6u   
 �   � 6:   �� $	 P01� 51      �� 52      "  6    .�53      ��  � ��$ Center=�6K   54       56      �� 57      " ��6    .�58      ��  �@��TextAlignment$ Right=�6>   �� 59       � ��$ Center=�6B   �� 5:       � {� . 6:   5;      5=        5>      $ TX Ba Đồn��6%   $ 80 6U    .  $
 Bold"�6s   
 �   � 68   �� $	 P02� 5?      �� 5@      "  6    .�5A      ��  � ��$ Center=�6K   5B       5D      �� 5E      " ��6    .�5F      ��  �@��TextAlignment$ Right=�6>   �� 5G       � ��$ Center=�6B   �� 5H       � {� . 6:   5I      5K        5M      $ Bố Trạch��6%   $ 80 6T    .  $
 Bold"�6r   
 �   � 67   �� $	 P03� 5N      �� 5O      "  6    .�5P      ��  � ��$ Center=�6K   5Q       5S      �� 5T      " ��6    .�5U      ��  �@��TextAlignment$ Right=�6>   �� 5V       � ��$ Center=�6B   �� 5W       � {� . 6:   5X      5Z        5\      $ Lệ Thủy��6%   $ 80 6S    .  $
 Bold"�6q   
 �   � 66   �� $	 P04� 5]      �� 5^      "  6    .�5_      ��  � ��$ Center=�6K   5`       5b      �� 5c      " ��6    .�5d      ��  �@��TextAlignment$ Right=�6>   �� 5e       � ��$ Center=�6B   �� 5f       � {� . 6:   5g      5i        5j      $ 
Hội Sở��6%   $ 80 6R    .  $
 Bold"�6p   
 �   � 65   �� $	 P05� 5k      �� 5l      "  6    .�5m      ��  � ��$ Center=�6K   5n       5p      �� 5q      " ��6    .�5r      ��  �@��TextAlignment$ Right=�6>   �� 5s       � ��$ Center=�6B   �� 5t       � {� . 6:   5u      5w        5x      $ Quảng Ninh��6%   $ 80 6V    .  $
 Bold"�6t   
 �   � 69   �� $	 P06� 5y      �� 5z      "  6    .�5{      ��  � ��$ Center=�6K   5|       5~      �� 5      " ��6    .�5�      ��  �@��TextAlignment$ Right=�6>   �� 5�       � ��$ Center=�6B   �� 5�       � {� . 6:   5�      5�        5�      $ 	Minh Hóa��6%   $ 80 6T    .  $
 Bold"�6r   
 �   � 67   �� $	 P07� 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Right=�6>   �� 5�       � ��$ Center=�6B   �� 5�       � {� . 6:   5�      5�        5�      $ Tuyên Hóa��6%   $ * 6U    .  $
 Bold"�6r   
 �   � 68   �� $	 P08� 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Right=�6>   �� 5�       � ��$ Center=�6B   �� 5�       � {� . 6:   5�      5�      5�      �� 5�   
   -   5         ��Name$ ChkAll��5�   `   $��
Tất cả��6   $
��auto��6$   $
��Left=�62   $��	24,29,0,0q�6M   $	��Top=�6�   $
��auto��6�   ���	#FFD81313$
��Bold"�6�    d� 	IsChecked$
 TrueY�6�   {�.  6  5�      5�      