   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfXLN_M3   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   G   -   5          � Title$)  #Cập nhật XLN theo mẫu 01/NĐH��5   	   $	��543��6.   $	��868��6;    � WindowStartupLocation$ CenterScreen=�6�    � 
ResizeMode$ NoResize=�6�    � SizeToContent$ Manual=�6�    � WindowStyle$
 None=�6�   .��5      �� .+�5   
   �� -   5        $��label1��5   �    �� Left$	 144��6   
 �� Top$ 82��6"   $7��1CẬP NHẬT XỬ LÝ NƠ THEO TỔ ĐÃ XỬ LÝ��62   $��16(�6_   $
��Bold"�6m   ���	#FF086908$��31��6�   $	��424��6�   �� 5   
   -   5        $��border1��5   y   
�������$��2q�6&   $ 10��6:   $ 45��6K    �� CornerRadius$ 20��6[   $��69��6m   $	��848��6�   ��5      �� 5	       �� EndPoint$ 0.5,1&�6&   	 �� 
StartPoint$	 0.5,0&�67   .��5
      � 
 � Color$
 White��6#    � Offset$ 0Q�61   � 5      $
 	#FFF0F0F0��6#   $ 1Q�65   � 5      $
 	#FFEAF0E5��6#   $ 0.543Q�65   5      ._�5      �� $��57��6   $	��798��6!   .+�5      �� -   5        $��
separator1��5   I   $	 156��6   $ 49��6.   $��2��6>   $	��480��6[   $��0q�6g   (  "System.Windows.Controls.DatePicker  5      -   5        $��dtpNgay��5      $ 40��6,   $ 18��6=   �� 5      -   5        $��lblNgay��5   '   $��Ngày��6   $
��auto��66   $
��auto��6C   $ 17��6Q   $ 1��6a   �� 5      -   5        $��CboPos��5   B   $��22��6   $
��Left=�6'   $	��Top=�6P   $	��114��6h   $	 182��6t   $ 17��6�   6�   �� 5      -   5        $��lblPos��5   M   $	��POS��6   $��23��6&   $
��Left=�62   $	��Top=�6[   $
��auto��6s   $	 147��6�   $ 14��6�   �� 5      -	   5        $��CboXa��5   B   $��22��6   $
��Left=�6'   $	��Top=�6O   $	��106��6g   $	 331��6t   $ 17��6�   �� 5      -
   5        $��lblXa��5   L   $	��Xã��6   $��23��6%   $
��Left=�61   $	��Top=�6Y   $
��auto��6q   $	 303��6~   $ 16��6�   �� 5      -   5        $��CboThang��5   B   $��22��6   $
��Left=�6'   $	��Top=�6R   $	��105��6j   $	 517��6v   $ 17��6�   �� 5      -   5        $��lblTo��5   O   $��Tháng��6   $��23��6(   $
��Left=�64   $	��Top=�6\   $
��auto��6t   $	 460��6�   $ 14��6�   �� 5      -   5        $��
lblGetData��5   \   $	 678��6   $ 15��6*   $��Lấy dữ liệu��6:   $��27��6P   $
��auto��6n   ��5      �� 5       �� 
TargetType" ��6     �� Triggers 5      P� 5   "    P� Property �@J�IsMouseOver P� Value . . �5   &   ��  �� Property � �� �� Value
 �  ��6K   �� 5   &    � ��$
 Bold"�6K   5    #   .�5"      ��  � ��
 � � �6C   �� 5#       � ��$ Comic Sans MS)�6C   5$      5&      5(      5)      �� 5*   
   -   5        $��label2��5*   �   $	 293��6   $ 14��6"   $8��2CẬP NHẬT XỬ LÝ NỢ THEO TỔ ĐÃ XỬ LÝ��62   $��Center=�6_   $��Center=�6z   $��16(�6�   $
��Bold"�6�   ���	#FF086908$��30��6�   $	��325��6�   &   System.Windows.Controls.DataGrid  5+   
   -   5        $��dgvData��5+   �      AlternatingRowBackground
 �����   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $ 12��6�   $	 122��6�      ColumnHeaderHeight$ 24Q�6�   $	��233��6�      HorizontalGridLinesBrush
 �����   RowBackground
 �����   	RowHeight$ 24Q�6     VerticalGridLinesBrush
 �����$	��846��6G     Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ Tên Tổ Trưởng��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 140 6[      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   ��   �� Path$  TENTT�!   HeaderStyle! 5.      �� 5/      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�50      ��  � ��$ Center=�6K   51      "   ElementStyle" 53      �� 54      " ��6    .�55      ��  �@��TextAlignment$
 Left=�6>   �� 56       � ��$ Center=�6B   57      59        5:      $ ĐVUT��6%   $ 80 6Q    .  63   �� $
  DVUT�! 5;      �� 5<      "  6    .�5=      ��  � ��$ Center=�6K   5>      " 5@      �� 5A      " ��6    .�5B      ��  �@��TextAlignment$ Center=�6>   �� 5C       � ��$ Center=�6B   5D      5F        5H      $ 
Trả Nợ��6%   $	 110 6T    .   65   �� $  TRANO�! 5I      �� 5J      "  6    .�5K      ��  � ��$ Center=�6K   5L      " 5N      �� 5O      " ��6    .�5P      ��  �@��TextAlignment$ Center=�6>   �� 5Q       � ��$ Center=�6B   5R      5T        5U      $ 	Lưu Vụ��6%   $	 110 6T    .   65   �� $  LUUVU�! 5V      �� 5W      "  6    .�5X      ��  � ��$ Center=�6K   5Y      " 5[      �� 5\      " ��6    .�5]      ��  �@��TextAlignment$ Center=�6>   �� 5^       � ��$ Center=�6B   5_      5a        5b      $ 	Gia Hạn��6%   $	 110 6V    .   66   �� $  GIAHAN�! 5c      �� 5d      "  6    .�5e      ��  � ��$ Center=�6K   5f      " 5h      �� 5i      " ��6    .�5j      ��  �@��TextAlignment$ Center=�6>   �� 5k       � ��$ Center=�6B   5l      5n        5o      $ Chuyển QH��6%   $	 110 6[    .   68   �� $  	CHUYEN_QH�! 5p      �� 5q      "  6    .�5r      ��  � ��$ Center=�6K   5s      " 5u      �� 5v      " ��6    .�5w      ��  �@��TextAlignment$ Center=�6>   �� 5x       � ��$ Center=�6B   5y      5{        5|      $ 
NQH Sau LD��6%   $ 90 6Y    .   69   �� $  NQH_LD�! 5}      �� 5~      "  6    .�5      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ 
KH Thu NQH��6%   $ * 6\    .   69   �� $  	KH_THUNQH�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�      5�        5�   
   -   5        $��	dgvTarGet��5�   �   
 ����� .   .   .  $ 12��6�   $	 371��6�   $ 24Q�6�   $	��146��6�   
 �����
 �����$ 24Q�6  
 �����$	��846��6I   5�        5�      $ Tên Tổ Trưởng��6%   $	 140 6[    .  6<   �� $  TENTT�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$
 Left=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ ĐVUT��6%   $ 80 6Q    .  63   �� $
  DVUT�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ 
Trả Nợ��6%   $	 110 6T    .   65   �� $  TRANO�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ 	Lưu Vụ��6%   $	 110 6T    .   65   �� $  LUUVU�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ 	Gia Hạn��6%   $	 110 6V    .   66   �� $  GIAHAN�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ Chuyển QH��6%   $	 110 6[    .   68   �� $  	CHUYEN_QH�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ NQH Sau LĐ��6%   $ 90 6Y    .   69   �� $  NQH_LD�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $ 
KH Thu NQH��6%   $ * 6\    .   69   �� $  	KH_THUNQH�! 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�      " 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5      5     5     �� 5  
   -   5        $��lblOk��5  R   $	 445��6   $	 517��6"   $��Cập Nhật��63   $��26��6F   $��69��6_   ��5     �� 5     " ��6    5	     P� 5
      �@J�IsMouseOver . . �5     ��  � ��
 �  ��6C   �� 5      � ��$
 Bold"�6C   5     .�5     ��  � ��
 � � �6;   �� 5      � ��$ Time New Roman)�6;   5     5     �� 5  
   -   5        $��lblChapNhan��5  S   $	 284��6   $	 517��6"   $��Chấp nhận��63   $��26��6G   $��78��6f   ��5     �� 5     " ��6    5     P� 5      �@J�IsMouseOver . . �5     ��  � ��
 �  ��6C   �� 5      � ��$
 Bold"�6C   5     .�5     ��  � ��
 � � �6;   �� 5      � ��$ Time New Roman)�6;   5      5"     �� 5#  
   -   5        $��	lblTuChoi��5#  Q   $	 376��6   $	 517��6"   $��Từ Chối��63   $��26��6E   $��62��6b   ��5$     �� 5%     " ��6    5&     P� 5'      �@J�IsMouseOver . . �5(     ��  � ��
 �  ��6C   �� 5)      � ��$
 Bold"�6C   5*     .�5,     ��  � ��
 � � �6;   �� 5-      � ��$ Time New Roman)�6;   5.     50     �� 53  
   -   5        $��lblClose��53  O   $	 529��6   $	 517��6"   $��Thoát��63   $��26��6C   $��45��6_   ��54     �� 55     " ��6    56     P� 57      �@J�IsMouseOver . . �58     ��  � ��
 �  ��6C   �� 59      � ��$
 Bold"�6C   5:     .�5<     ��  � ��
 � � �6;   �� 5=      � ��$ Time New Roman)�6;   5>     5@     5A     5B     