   M S B A M L   `   `   `  ����  Presentation     Presentation.WpfTSCCKiemKe   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   	   -   5          � Title$&   Bão trì thiết bị tin học��5   	   $��170.4��6*   $��499.839��69    � WindowStartupLocation$  CenterScreen=�6I    � Icon$ Icons/My Video.ico��6p   .��5      � .+�5   
   �� -   5         ��Name$ btnClose��5      $��Close��6   $
��Left=�6!   $��24��6<   $��
257,89,0,0q�6H   $	��Top=�6\   $��84��6t   ��5	      �� 5
       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5       �� 
TargetType" ��6.   .R�5   "   ��  ��Name$ border��6)   $��1q�69   	 �� Padding$		 4,2q�6M   
�������
 �� CornerRadius$
 3��6r   #��z��6�   ._�5   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
 �/���6;   5      5      5      �� 5   
   -   5        $ btnSua��5   |   $��OK��6   $
��Left=�6   $��24��69   $��
151,89,0,0q�6E   $	��Top=�6Y   $��84��6q   ��5       �� 5!      " ��6   .�5"      ��  � �� 5#      �� 5$      " ��6.   .R�5%   "   �� $ border��6)   $��1q�69   $		 4,2q�6M   
�������$
 3��6r   #��z��6�   ._�5&   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5'   #   5(      5*       5+      P� 5,       �@J�IsMouseOver . . �5-      ��  � ��
 �/���6;   5.      50      53      �� 54   
   -   5         ��Name$ lblPhong��54   #   $��Phòng/Tổ��6   $
��Left=�65   $
��auto��6P   $��
276,14,0,0q�6^   $	��Top=�6r   $
��auto��6�   �� 55   
   -   5         ��Name$ CboPhong��55   N   $��23��6   $
��Left=�6   $��
340,14,0,0q�6:   $	��Top=�6`   $��98��6x   6�   �� 57   
   -   5        $ CboPos��57   N   $��22��6   $
��Left=�6   $��
182,15,0,0q�6:   $	��Top=�6^   $��89��6v   6�   $  Telerik.Windows.Controls.Label  58   
   -   5          Name$ LblPos��58   a   $	��POS��6   $��21��6&   $
��Left=�62   $��
151,15,0,0q�6M   $	��Top=�6q   $��38��6�     59   
   -   5        $ LblNgay��59   b   $��Ngày��6   $
��auto��6'   $
��Left=�65   $��7,14,0,0q�6P   $	��Top=�6s   $
��auto��6�   (  "System.Windows.Controls.DatePicker  5:   
   -	   5          Name$ dtpNgay��5:   O   $��21��6   $
��Left=�6!   $��	47,15,0,0q�6<   $	��Top=�6`   $	��102��6x   5<      5=      