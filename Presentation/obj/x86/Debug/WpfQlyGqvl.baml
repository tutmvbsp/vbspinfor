   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfQlyGqvl   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   	   -   5          � Title$#  Quản lý nguồn vốn GQVL��5   	   $��204.328��6(   $��414.179��69    � WindowStartupLocation$  CenterScreen=�6I    � Icon$ Icons/Network_b.ico��6�   .��5      � .+�5   
   (  "System.Windows.Controls.DatePicker  -   5          Name$ dtpNgay��5   P   $��22��6   $
��Left=�6!   $��
135,16,0,0q�6<   $	��Top=�6a   $	��103��6y   �� 5   
   -   5         ��Name$ lblNgay��5   a   $��Ngày số liệu��6   $��29��6'   $
��Left=�63   $��	54,13,0,0q�6N   $	��Top=�6r   $
��auto��6�   �� 5	   
   -   5        $ lblPos��5	   Z   $	��POS��6   $
��auto��6   $
��Left=�6,   $��	64,47,0,0q�6G   $	��Top=�6j   $
��auto��6�   �� 5
   
   -   5         ��Name$ CboPos��5
   N   $��20��6   $
��Left=�6   $��
122,48,0,0q�6:   $	��Top=�6^   $	��125��6v   6�   �� 5   
   -   5        $ CboXa��5   N   $��20��6   $
��Left=�6   $��
122,73,0,0q�6:   $	��Top=�6]   $	��125��6u   �� 5   
   -   5        $ CboCqlv��5   N   $��21��6   $
��Left=�6   $��
122,98,0,0q�6:   $	��Top=�6_   $	��125��6w   �� 5   
   -   5        $ lblXa��5   W   $	��Xã��6   $��23��6   $
��Left=�6)   $��	66,72,0,0q�6D   $	��Top=�6f   $
��auto��6~   �� 5   
   -	   5        $ lblCqlv��5   \   $��	Cấp QLV��6   $��28��6"   $
��Left=�6.   $��	64,91,0,0q�6I   $	��Top=�6m   $
��auto��6�   �� 5   
   -
   5         ��Name$ btnclose��5   ]   $��Close��6   $��22��6!   $
��Left=�6-   $��241,134,0,0q�6H   $	��Top=�6o   $��91��6�   6�   �� 5   
   -   5        $ btnOk��5   Z   $��OK��6   $��22��6   $
��Left=�6*   $��118,134,0,0q�6E   $	��Top=�6i   $��91��6�   6�   '  !Telerik.Windows.Controls.GroupBox  5   
   -   5          Name$ groupBox��5      $
��Left=�6-   $��71��6H   $��
271,35,0,0q�6T   $	��Top=�6h   $��88��6�   $��Mẫu��6�   .��5      �  � ColumnDefinitions 5      �� 5      $	��21* �6'   �� 5      $	��55* �6'   .+�5      	� -   5        	 	�Name$	 Ration1��5      $��Xã/Tổ��6/   $
��Left=�6?   $	��Top=�6Z   $��0,9,0,0q�6r   $��2��6�   
 d� 	IsChecked$

 TrueY�6�   	� 5      -   5        $	 Ration2��5      $��	Cấp QLV��6/   $
��Left=�6A   $	��Top=�6\   $��0,27,0,0q�6t   $��2��6�   $��0��6�   5      5      5      5      