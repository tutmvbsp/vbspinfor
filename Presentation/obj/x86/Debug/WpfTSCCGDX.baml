   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfTSCCGDX   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   	   -   5          � Title$'  !Cập nhật TSCC giao dịch xã��5   	   $	��569��6,   $��1177.839��69    � WindowStartupLocation$  CenterScreen=�6J    � Icon$ Icons/My Video.ico��6q   .��5      � .+�5   
   �� -   5         ��Name$ btnClose��5   �   $��Close��6   $
��Left=�6!   $��24��6<   $��601,500,0,0q�6H   $	��Top=�6]   $��84��6u   ��5	      �� 5
       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5       �� 
TargetType" ��6.   .R�5   "   ��  ��Name$ border��6)   $��1q�69   	 �� Padding$		 4,2q�6M   
�������
 �� CornerRadius$
 3��6r   #��z��6�   ._�5   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
 �/���6;   5      5      5      �� 5   
   -   5        $ btnSua��5   �   $��Update��6   $
��Left=�6"   $��24��6=   $��495,500,0,0q�6I   $	��Top=�6^   $��84��6v   ��5       �� 5!      " ��6   .�5"      ��  � �� 5#      �� 5$      " ��6.   .R�5%   "   �� $ border��6)   $��1q�69   $		 4,2q�6M   
�������$
 3��6r   #��z��6�   ._�5&   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5'   #   5(      5*       5+      P� 5,       �@J�IsMouseOver . . �5-      ��  � ��
 �/���6;   5.      50      53      &   System.Windows.Controls.DataGrid  57   
   -   5          Name$ dgvData��57   �      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     	RowHeight$ 30Q�6b   $	��356��6t   $
��Left=�6�   $��	10,49,0,0q�6�   $	��Top=�6�   $
��1151��6�      CanUserResizeRows .     Columns 59      4  .System.Windows.Controls.DataGridCheckBoxColumn  5:      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .     CanUserReorder .     CanUserResize .     CanUserSort .     Header$ GD Xã��6�      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ��  �� Path$	 GDX�   	CellStyle 5;      �� 5<      *  $System.Windows.Controls.DataGridCell"  6    .�5=      ��  � ��$ Center=�6B   5>      5@      0  *System.Windows.Controls.DataGridTextColumn  5B      $ 	Mã TS/CC��6%   $	 150 6Q    .  67   �� *MA_TS+   HeaderStyle 5C      �� 5D      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5E      ��  � ��$ Center=�6K   5F          ElementStyle  5H      �� 5I      " ��6    .�5J      ��  �@��TextAlignment$
 Left=�6>   �� 5K       � ��$ Center=�6B   5M      5O        5P      $ 
Mã nhản��6%   $ 60 6Y    .  66   �� *MA_NHANHIEU_TS+ 5Q      �� 5R      "  6    .�5S      ��  � ��$ Center=�6K   5T        5V      �� 5W      " ��6    .�5X      ��  �@��TextAlignment$
 Left=�6>   �� 5Y       � ��$ Center=�6B   5[      5]        5_      $ 
Tên TS/CC��6%   $	 250 6S    .  68   �� *TEN_TS+ 5`      �� 5a      "  6    .�5b      ��  � ��$ Center=�6K   5c        5e      �� 5f      " ��6    .�5g      ��  �@��TextAlignment$
 Left=�6>   �� 5h       � ��$ Center=�6B   5i      5k        5m      $ Cán bộ QL��6%   $	 130 6V    .  68   �� *	CB_QUANLY+ 5n      �� 5o      "  6    .�5p      ��  � ��$ Center=�6K   5q        5s      �� 5t      " ��6    .�5u      ��  �@��TextAlignment$ Center=�6>   �� 5v       � ��$ Center=�6B   5w      5y        5z      $ Mô tã��6%   $ * 6M    .  64   �� *MOTA+ 5{      �� 5|      "  6    .�5}      ��  � ��$ Center=�6K   5~        5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $! Đề xuất- kiến nghị��6%   $	 390 6]    .   6A   �� *	DE_NGHI+ 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�        5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�      5�      �� 5�   
   -   5        ! ��Name$! lblPhong��5�   #   $��Phòng/Tổ��6   $
��Left=�65   $
��auto��6P   $��
276,14,0,0q�6^   $	��Top=�6r   $
��auto��6�   *	 
 $Telerik.Windows.Controls.RadComboBox	  5�   
   -   5        " 	 Name$" CboPhong��5�   Y   $��23��6   $
��Left=�6*   $��
340,14,0,0q�6E   $	��Top=�6k   $��98��6�   6�   	  5�   
   -   5        $" CboPos��5�   Y   $��22��6   $
��Left=�6*   $��
182,15,0,0q�6E   $	��Top=�6i   $��89��6�   6�   $
  Telerik.Windows.Controls.Label
  5�   
   -   5        # 
 Name$# LblPos��5�   a   $	��POS��6   $��21��6&   $
��Left=�62   $��
151,15,0,0q�6M   $	��Top=�6q   $��38��6�   
  5�   
   -	   5        $# LblNgay��5�   b   $��Ngày��6   $
��auto��6'   $
��Left=�65   $��7,14,0,0q�6P   $	��Top=�6s   $
��auto��6�   (  "System.Windows.Controls.DatePicker  5�   
   -
   5        $  Name$$ dtpNgay��5�   O   $��21��6   $
��Left=�6!   $��	47,15,0,0q�6<   $	��Top=�6`   $	��102��6x   �� 5�   
   -   5        $! ShowGrid��5�   U   $��	Show Grid��6   $
��Left=�6$   $��554,16,0,435q�6?   % J� RenderTransformOrigin$% 1.342,-2.636&�6�   $��67��6�   z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      & �� Angle$& 0.591Q�6&   T� 5�      ' T� X$' 1.127Q�6)   ( T� Y$( 0.854Q�63   5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��
 � � �63   �� 5�       � ��$
 Bold"�63   �� 5�       � ��$ Times New Roman)�63   �� 5�       �@�ToolTip$/ )Click vào đây để nhập số liệu��60    5�      P� 5�       �@J�IsMouseOver . . �5�      ��  � ��
 � � �6;   �� 5�       � ��$ Italic$�6;   �� 5�       � ��$
 Bold"�6;   5�      5�      5�      �� 5�   
   -   5        ) ��Name$) chkTh��5�      $��Tổng hợp��6"   $
��Left=�65   $��22��6P   $��
454,15,0,0q�6\   $	��Top=�6p   $��77��6�   �� 5�   
   -   5        * ��Name$* 	textBlock��5�      $
��Left=�6'   $��62��6B   $��
35,420,0,0q�6N   $
��Wrap=�6b   $�� ��6v   $	��Top=�6~   $
��1109��6�   5�      5�      