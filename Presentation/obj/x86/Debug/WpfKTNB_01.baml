   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PresentationPresentation  5   	       Presentation.WpfKTNB_01   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   	   ;presentationclr-namespace:Presentation    	 
    5   	   -   5          � Title$>  8Báo cáo thống kê số lượt kiểm tra giám sát��5   	   $��577.497��6<   $��853.83��6M    � WindowStartupLocation$ CenterScreen=�6\    � Icon$ /Icons/Connected.ico��6�   .��5      � $��0,0,2,0q�6   .+�5	   
   (  "System.Windows.Controls.DatePicker  -   5          Name$ dtpNgay��5	   O   $��26��6   $
��Left=�6!   $��	73,19,0,0q�6<   $	��Top=�6`   $	��110��6x   �� 5
   
   -   5         ��Name$ lblNgay��5
   [   $��Ngày��6   $
��auto��6   $
��Left=�6-   $��	23,19,0,0q�6H   $	��Top=�6l   $
��auto��6�   �� 5   
   -   5        $ lblPos��5   [   $	��POS��6   $
��auto��6   $
��Left=�6,   $��
197,21,0,0q�6G   $	��Top=�6k   $
��auto��6�   * 
 $Telerik.Windows.Controls.RadComboBox  5   
   -   5          Name$ CboPos��5   Y   $��22��6   $
��Left=�6*   $��
226,22,0,0q�6E   $	��Top=�6i   $	��110��6�   �� 5   
   -   5         ��Name$ btnClose��5   \   $��Đóng��6   $��22��6    $
��Left=�6,   $��607,506,0,0q�6G   $	��Top=�6n   $��90��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � ��	 �� Value	 5      �� 5      
 �� 
TargetType"
 ��6.   .R�5   "   ��  ��Name$ border��6)   $��1q�69    �� Padding$	 4,2q�6M   
������� �� CornerRadius$ 3��6r   #��z��6�   ._�5   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
	 �/���6;   5      5      5       �� 5!   
   -   5        $ btnOk��5!   `   $��Cập Nhật��6   $��22��6$   $
��Left=�60   $��476,506,0,0q�6K   $	��Top=�6o   $��90��6�   ��5"      �� 5#      " ��6   .�5$      ��  � ��	 5%      �� 5&      "
 ��6.   .R�5'   "   �� $ border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�5(   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5)   #   5*      5,       5-      P� 5.       �@J�IsMouseOver . . �5/      ��  � ��
	 �/���6;   50      52      54      �� 55   
   -   5        $��lblNhap��55   #   $��Nhập mới��6   $��Center=�6V   $��Center=�6z   $��663,10,113,498q�6�   $��68��6�   ��56      �� 57      " ��6   .�58      ��  � ��
	 ��  �63   �� 59       � ��$	 Comic Sans MS)�63   �� 5:       � ��$
	 Bold"�63   �� 5;       � ��$	 Center=�6<    5<      P� 5=       �@J�IsMouseOver . . �5>      ��  � ��
	 � � �6;   �� 5?       � ��$
	 Bold"�6;   5@      5B      5E      �� 5F   
   -	   5        $��lblSua��5F      $
��Xóa��6   $��Center=�6O   $��Center=�6s   $��747,10,59,498q�6�   $��38��6�   ��5G      �� 5H      " ��6   .�5I      ��  � ��
	 ��  �63   �� 5J       � ��$	 Comic Sans MS)�63   �� 5K       � ��$
	 Bold"�63   �� 5L       � ��$	 Center=�6<    5M      P� 5N       �@J�IsMouseOver . . �5O      ��  � ��
	 � � �6;   �� 5P       � ��$
	 Bold"�6;   5Q      5S      5U      �� 5W   
   $��350,6,360,500q�6   .+�5X      �� $��Cấp báo cáo��6   $
��Bold"�6+   �� 5Y       �� Orientation$ 
Horizontal=�6   .+�5Z      	� -
   5         	� 	GroupName$	 cap��5Z      $
��PGD ��6.   
 � Tag$
 Opt1��6=    d� 	IsChecked$
 TrueY�6H   6Y   	� 5[      -   5        $	 cap��5[      $��Cán bộ KS��6.   $
 Opt2��6B   $��90��6M   6X   5\      5]      �� 5^   
   $��480,6,210,500q�6   .+�5_      �� $��Kỳ báo cáo��6   $
��Bold"�6*   �� 5`      $ 
Horizontal=�6   .+�5a      	� -   5        $ ky��5a      $��Tháng ��6-   $
 Opt3��6>   $
 TrueY�6I   6[   	� 5b      -   5        $ ky��5b      $��Quý ��6-   $
 Opt4��6<   6G   	� 5c      -   5        $ ky��5c      $
��Năm��6-   $
 Opt5��6;   6F   5d      5e      &   System.Windows.Controls.DataGrid  5f   
   -   5          Name$ dgvData��5f   �      SelectionMode$ Single=�6+      AutoGenerateColumns .  $	��435��6^      CanUserAddRows .     CanUserDeleteRows .  $
��Left=�6�   $��	10,58,0,0q�6�   $	��Top=�6�   $	��834��6�   ��6   ��    	CellStyle 5g      �� 5h      *  $System.Windows.Controls.DataGridCell"  6    5i      �� 5j       �� Binding 6&   �� *INDAM+ �� Value BOLD6@   .a�5k      ��  � ��
	 ��  �6;   �� 5l       � ��$
	 Bold"�6;   5m      �� 5n       6&   �� *	KT_AUTH+ 06B   .a�5o      ��  � {�	 .  6:   5p      5s         Columns 5u      0  *System.Windows.Controls.DataGridTextColumn  5v      ,  &System.Windows.Controls.DataGridColumn   Header$ TT��6%       MinWidth$  40Q�6O   1  +System.Windows.Controls.DataGridBoundColumn!   Binding! 61   �� *	KT_STT_HT+"   HeaderStyle" 5w      �� 5x      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5y      ��  � ��$	 Center=�6K   5z      #   ElementStyle# 5|      �� 5}      " ��6    .�5~      ��  �@��TextAlignment$	 Right=�6>   �� 5       � ��$	 Center=�6B   �� 5�       � {�	 . 6:   5�      5�        5�      $ Đối tượng��6%   $   Width5	  /System.Windows.Controls.DataGridLengthConverter$	$ 250	 6S   %   
IsReadOnly% . ! 68   �� *KT_DKT+" 5�      �� 5�      "  6    .�5�      ��  � ��$	 Center=�6K   5�      # 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$
	 Left=�6>   �� 5�       � ��$	 Center=�6B   �� 5�       � {�	 . 6:   5�      5�        5�      $ Số lượt
   tỉnh��6%   $$ 50	 6[   % .  ! 6B   �� *COT3+" 5�      �� 5�      "  6    .�5�      ��  � ��$	 Center=�6K   5�      # 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$	 Center=�6>   �� 5�       � ��$	 Center=�6B   �� 5�       � {�	 . 6:   5�      5�        5�      $ Số lượt
 huyện��6%   $$ 70	 6Z   % .  ! 6A   �� *COT4+" 5�      �� 5�      "  6    .�5�      ��  � ��$	 Center=�6K   5�      # 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$	 Center=�6>   �� 5�       � ��$	 Center=�6B   �� 5�       � {�	 . 6:   5�      5�        5�      $- '  Số lượt
 điểm giao
    dịch��6%   $$ 70	 6m   % .  ! 6T   �� *COT5+" 5�      �� 5�      "  6    .�5�      ��  � ��$	 Center=�6K   5�      # 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$	 Center=�6>   �� 5�       � ��$	 Center=�6B   �� 5�       � {�	 . 6:   5�      5�        5�      $  Số lượt
 tổ TKVV��6%   $$ 70	 6]   % .  ! 6D   �� *COT6+" 5�      �� 5�      "  6    .�5�      ��  � ��$	 Center=�6K   5�      # 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$	 Center=�6>   �� 5�       � ��$	 Center=�6B   �� 5�       � {�	 . 6:   5�      5�        5�      $ Ghi chú��6%   $$ *	 6R   % .  ! 67   �� *GHICHU+" 5�      �� 5�      "  6    .�5�      ��  � ��$	 Center=�6K   �� 5�       � ��$	 Center=�6I   5�      # 5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$	 Center=�6>   �� 5�       � ��$	 Center=�6B   �� 5�       � {�	 . 6:   5�      5�      5�      5�      5�      