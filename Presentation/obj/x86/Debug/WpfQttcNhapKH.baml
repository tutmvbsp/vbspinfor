   M S B A M L   `   `   `  ����  Presentation     Presentation.WpfQttcNhapKH   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$#  Nhập một số chỉ tiêu��5   	   $	��648��6&   $��881.6��63    � WindowStartupLocation$  CenterScreen=�6A   .��5      � .+�5   
   �� -   5        $��btnClose��5   ]   $��Thoát��6   $��27��6!   $
��Left=�6-   $��689,574,0,0q�6H   $	��Top=�6m   $��98��6�   6�   �� 5   
   -   5        $��	btnUpdate��5   [   $
��Lưu��6   $��27��6   $
��Left=�6+   $��562,574,0,0q�6F   $	��Top=�6l   $��98��6�   6�   �� 5   
   -   5        $��btnLoad��5   `   $��Thay đổi��6   $��27��6$   $
��Left=�60   $��442,574,0,0q�6K   $	��Top=�6o   $��98��6�   6�   &   System.Windows.Controls.DataGrid  5	   
   -   5        $��dgvData��5	   j      AutoGenerateColumns .  $	��231��6/   $
��Left=�6<   $��	12,53,0,0q�6W   $	��Top=�6y   $	��854��6�      CanUserAddRows .     CanUserDeleteRows .     AlternatingRowBackground
 �����   RowBackground
 �����   	RowHeight$ 24Q�6	     HorizontalGridLinesBrush
 �����	   VerticalGridLinesBrush
	 �����
   ColumnHeaderHeight$
 24Q�6b   � 	Resources 5
      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%|   '        �� 5      "  6   .�5      ��  � ��
 �����63   �� 5       � ��$ Center=�6C   5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$	 STT��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 20 6K      FontSize$ 13Q�6V      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *STT+6e     5      $ Chỉ Tiêu��6%   $	 700 6T   $ 13Q�6`    .  67   �� *	CHITIEU+6o     5      $ Kế hoạch��6%   $ 90 6O   $ 13Q�6Z    67   �� *KH+6Z   5        5   
   -   5        $��	dgvTarGet��5   k    .  $	��179��6/   $
��Left=�6<   $��
12,371,0,0q�6W   $	��Top=�6|   $	��854��6�    .   .  
 �����
 �����$ 24Q�6  
 �����
	 �����$
 24Q�6e   5      ��%|   '        �� 5      "  6   .�5      ��  � ��
 �����63   �� 5       � ��$ Center=�6C   5      5       5        5      $	 STT��6%   $ 20 6K   $ 13Q�6V    62   �� *STT+6V     5      $ Chỉ Tiêu��6%   $	 700 6T   $ 13Q�6`    67   �� *	CHITIEU+6`     5       $ Kế hoạch��6%   $ 90 6O   $ 13Q�6Z    67   �� *KH+6Z   5"      �� 5#   
   -   5        $��label1��5#   d   $��Gia trị ban đầu��6   $��39��6*   $
��Left=�66   $��	263,9,0,0q�6Q   $	��Top=�6r   $	��322��6�   $��16(�6�   $
��Bold"�6�   
���"�"�$��Center=�6�   �� 5$   
   -   5        $��label2��5$   g   $��Gia trị thay đổi��6   $��34��6+   $
��Left=�67   $��263,308,0,0q�6R   $	��Top=�6u   $	��322��6�   $��16(�6�   $
��Bold"�6�   
���"�"�$��Center=�6�   5%      5&      