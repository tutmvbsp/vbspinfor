   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfDanhSach   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   �   -   5          � Title$*  $Danh Sách Hộ Nghèo, Cận Nghèo��5   	   $	��248��6/   $	��510��6<    � Icon$ /ICONS/VBSP.ico��6H    � WindowStartupLocation$ CenterScreen=�6_   .��5      � $	��206��6    � RowDefinitions 5   
   �� 5      $
��135* �6   �� 5      $	��11* �6   �� 5	      $
��266* �6   .+�5   
   �� -   5        $��btnThoat��5   \   $��Thoát��6   $��31��6!   $
��Left=�6-   $��
285,60,0,0q�6H   $	��Top=�6l   $	��100��6�   $��2��6�   �� 5   
   -   5        $��btnChapNhan��5   `   $��Chấp Nhận��6   $��30��6%   $
��Left=�61   $��
149,60,0,0q�6L   $	��Top=�6s   $	��106��6�   $��2��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dtpNgay��5   P   $��26��6   $
��Left=�6!   $��
128,25,0,0q�6<   $	��Top=�6_   $	��103��6w   $  Telerik.Windows.Controls.Label  5   
   -   5        $��lblNgay��5   f   $��Chọn Ngày��6   $��21��6,   $
��Left=�68   $��	28,33,0,0q�6S   $	��Top=�6u   $��93��6�   
��� � �$��14(�6�     5   
   -   5        $��lblPos��5   g   $��
Chọn POS��6   $��21��6+   $��Right=�67   $��
0,17,168,0q�6S   $	��Top=�6u   $��73��6�   
��� � �$��14(�6�     5   
   -   5        $��lblXa��5   f   $��
Chọn Xã��6   $��21��6*   $��Right=�66   $��
0,45,167,0q�6R   $	��Top=�6s   $��73��6�   
��� � �$��14(�6�     5   
   -   5        $��dtpStartDate��5   F   $
��Left=�6   $��128,62,0,120q�60   $	��102��6Z   $��3��6f     5   
   -	   5        $��
dtpEndDate��5   P   $��26��6   $
��Left=�6!   $��
128,19,0,0q�6<   $	��Top=�6b   $	��102��6z   $��2��6�   �� 5   
   -
   5        $��cboPos��5   N   $��26��6   $
��Left=�6   $��
336,11,0,0q�6:   $	��Top=�6\   $	��124��6t   6�   �� 5   
   -   5        $��cboXa��5   N   $��26��6   $
��Left=�6   $��
336,40,0,0q�6:   $	��Top=�6[   $	��124��6s   	� 5   
   -   5        $��opt01��5   u    	� 	GroupName$
 Chon��6   $��Hộ Nghèo��6'   $��21��6:   $
��Left=�6F   $��
256,19,0,0q�6a   $	��Top=�6�   $	��120��6�    d� 	IsChecked$ FalseY�6�   
����  �
��� � �
���  ��{�. $��14(�6  $��2��6  	� 5   
   -   5        $��opt19��5   y   $
 Chon��6   $��Hộ Cận Nghèo��6'   $��21��6>   $
��Left=�6J   $��
346,19,0,0q�6e   $	��Top=�6�   $	��114��6�   
����  �
��� � �
���  ��$��14(�6�   $��2��6�   �� 5   
   -   5        $��lblStartDate��5   \   $��
Từ Ngày��6   $��24��6"   $
��Left=�6.   $��	34,64,0,0q�6I   $	��Top=�6p   $��74��6�   
��� � �$��14(�6�   $��3��6�   �� 5   
   -   5        $��
lblEndDate��5   _   $��Đến Ngày��6   $��21��6#   $��Right=�6/   $��
0,19,378,0q�6K   $	��Top=�6q   $��76��6�   
��� � �$��14(�6�   $��2��6�   5      5      