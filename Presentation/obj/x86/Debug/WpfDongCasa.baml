   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfDongCasa   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   8   -   5          � Title$  Đóng CASA 105��5   	   $	��543��6   $	��868��6,    � WindowStartupLocation$ CenterScreen=�6�    � 
ResizeMode$ NoResize=�6�    � SizeToContent$ Manual=�6�    � WindowStyle$
 None=�6�   .��5      �� .+�5   
   �� -   5        $��label1��5   �    �� Left$	 144��6   
 �� Top$ 82��6"   $7��1THÔNH BÁO DANH SÁCH TỔ VIÊN RA KHỎI TTKVV��62   $��16(�6f   $
��Bold"�6t   ���	#FF086908$��31��6�   $	��424��6�   �� 5   
   -   5        $��border1��5   y   
�������$��2q�6&   $ 10��6:   $ 56��6K    �� CornerRadius$ 20��6[   $��69��6m   $	��848��6�   ��5      �� 5	       �� EndPoint$ 0.5,1&�6&   	 �� 
StartPoint$	 0.5,0&�67   .��5
      � 
 � Color$
 White��6#    � Offset$ 0Q�61   � 5      $
 	#FFF0F0F0��6#   $ 1Q�65   � 5      $
 	#FFEAF0E5��6#   $ 0.543Q�65   5      ._�5      �� $��57��6   $	��798��6!   .+�5      �� -   5        $��
separator1��5   H   $ 98��6   $ 46��6-   $��2��6=   $	��480��6Z   (  "System.Windows.Controls.DatePicker  5      -   5        $��dtpNgay��5      $ 40��6,   $ 18��6=   �� 5      -   5        $��lblNgay��5   '   $��Ngày��6   $
��auto��66   $
��auto��6C   $ 17��6Q   $ 1��6a   �� 5      -   5        $��CboPos��5   B   $��22��6   $
��Left=�6'   $	��Top=�6P   $	��114��6h   $	 182��6t   $ 17��6�   6�   �� 5      -   5        $��lblPos��5   M   $	��POS��6   $��23��6&   $
��Left=�62   $	��Top=�6[   $
��auto��6s   $	 147��6�   $ 14��6�   �� 5      -	   5        $��CboXa��5   B   $��22��6   $
��Left=�6'   $	��Top=�6O   $	��106��6g   $	 331��6t   $ 17��6�   6�   �� 5      -
   5        $��lblXa��5   L   $	��Xã��6   $��23��6%   $
��Left=�61   $	��Top=�6Y   $
��auto��6q   $	 303��6~   $ 16��6�   �� 5      -   5        $��CboTo��5   B   $��22��6   $
��Left=�6'   $	��Top=�6O   $	��105��6g   $	 478��6s   $ 17��6�   �� 5      -   5        $��lblTo��5   L   $
��Tổ��6   $��23��6%   $
��Left=�61   $	��Top=�6Y   $
��auto��6q   $	 447��6~   $ 16��6�   �� 5      -   5        $��ChkAll��5   U   $��
Tất cả��6   $
��auto��6,   $
��Left=�6:   $	��Top=�6c   $
��auto��6{   ���	#FFD81313$
��Bold"�6�   $ 82��6�   $ -9��6�   �� 5      $��Export��6   $
��Left=�6+   $��46��6F   $	��Top=�6R   $	��145��6j   $	 585��6v   $ 2��6�   .��5      � $��0,0,-2,0q�6    � ColumnDefinitions 5      �� 5      $��9* �6/   �� 5      $��2* �6/   .+�5       	� -   5        $
��Opt1��5    3   $��In��6&   $
��Left=�6?   $��21��6Z   $��-1,-10,0,-1q�6f   $	��Top=�6{   $
��auto��6�    d� 	IsChecked$
 TrueY�6�   	� 5!      -   5        $
��Opt3��5!   5   $
��Date��6&   $
��Left=�6A   $��21��6\   $��79,-10,0,-1q�6h   $	��Top=�6}   $
��auto��6�   $��0��6�   $��2��6�   	� 5"      -   5        $
��Opt2��5"   6   $��Excel��6&   $��21��6B   $��0,-10,28,-1q�6N   $	��Top=�6c   $��44��6{   $��0��6�   $��Right=�6�   5$      5%      �� 5'      -   5        $��
lblGetData��5'   \   $	 737��6   $ 15��6*   $��Lấy dữ liệu��6:   $��27��6P   $
��auto��6n   ��5(      �� 5)       �� 
TargetType" ��6     �� Triggers 5*      P� 5+   "    P� Property �@J�IsMouseOver P� Value . . �5,   &   ��  �� Property � �� �� Value
 �  ��6K   �� 5-   &    � ��$
 Bold"�6K   5.   #   .�50      ��  � ��
 � � �6C   �� 51       � ��$ Comic Sans MS)�6C   52      54      �� 55      -   5        $��lblSaoKe��55   d   $	 658��6   $ 70��6*   $��Sao kê KH hết dư nợ��6:   $��28��6X   $
��auto��6t   ��56      �� 57      " ��6     58      P� 59   "    �@J�IsMouseOver . . �5:   &   ��  � ��
 �  ��6K   �� 5;   &    � ��$
 Bold"�6K   5<   #   .�5>      ��  � ��
 � � �6C   �� 5?       � ��$ Comic Sans MS)�6C   5@      5B      5D      5E      �� 5F   
   -   5        $��label2��5F   �   $ 79��6   $ 20��6!   $c��]THÔNG BÁO DANH SÁCH TỔ VIÊN RA KHỎI TỔ VÀ ĐỀ NGHỊ LÀM THỦ TỤC ĐÓNG 105��61   $��16(�6�   $
��Bold"�6�   ���	#FF086908$��31��6�   $	��680��6�   &   System.Windows.Controls.DataGrid  5G   
   -   5        $��dgvData��5G   �      AlternatingRowBackground
 �����   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $ 12��6�   $	 167��6�      ColumnHeaderHeight$ 24Q�6�   $	��322��6�      HorizontalGridLinesBrush
 �����   RowBackground �	#FFD0FFFF   	RowHeight$ 24Q�6     VerticalGridLinesBrush
 �����$	��846��6J     Columns 5H      4  .System.Windows.Controls.DataGridCheckBoxColumn  5I      ,  &System.Windows.Controls.DataGridColumn   CanUserReorder .     CanUserResize .      CanUserSort  .  !   Header$! Chọn��6�   "   Width5  /System.Windows.Controls.DataGridLengthConverter$" 45 6�   1  +System.Windows.Controls.DataGridBoundColumn#   Binding# 6)   �� $ �� Path$
$ CHON�6�   0  *System.Windows.Controls.DataGridTextColumn  5J      $! MÃ KH��6F   $	" 110 6U   # 6%   �� $$ CS_MAKH�6U     5K      $! TÊN KHÁCH HÀNG��6G   $" * 6_   # 6%   �� $$ KH_TENKH�6_   4  .System.Windows.Controls.DataGridTemplateColumn  5L      $! Mẫu 10C/TD ngày��6)   $	" 145 6B   %   CellTemplate% 5M      �� 5N      .R�5P      �� $��Verdana)�6c   ��6(   �� *
NGAYDONG+& �� StringFormat$& {0:dd/MM/yyyy}��6c   5Q      '   CellEditingTemplate' 5S      �� 5T      .R�5U        (   SelectedDate( 6)   �� *
NGAYDONG+5V      5X        5o      $! TÀI KHOẢN 105��6H   $	" 150 6_   # 6%   �� $$ 	CS_SO_TK2�6_     5p      $! 	SỐ DƯ ��6I   $	" 120 6Y   # 6%   �� $$ 
CS_SODU_TK�6Y   5r      �� 5s   
   -   5        $��lblOk��5s   V   $	 376��6   $	 510��6"   $��In danh sách��63   $��27��6J   $��95��6c   ��5t      �� 5u      " ��6    5v      P� 5w       �@J�IsMouseOver . . �5x      ��  � ��
 �  ��6C   �� 5y       � ��$
 Bold"�6C   5z      .�5|      ��  � ��
 � � �6;   �� 5}       � ��$ Comic Sans MS)�6;   5~      5�      �� 5�   
   -   5        $��lblClose��5�   O   $	 481��6   $	 510��6"   $��Thoát��63   $��27��6C   $��45��6_   ��5�      �� 5�      " ��6    5�      P� 5�       �@J�IsMouseOver . . �5�      ��  � ��
 �  ��6C   �� 5�       � ��$
 Bold"�6C   5�      .�5�      ��  � ��
 � � �6;   �� 5�       � ��$ Comic Sans MS)�6;   5�      5�      �� 5�   
   -   5        ) ��Name$) lblGmt��5�      $��Mẫu 01/GMT��6#   $
��auto��68   $
��auto��6F   $	 576��6S   $	 139��6e   $
��Bold"�6v   
����  �6�   5�      5�      