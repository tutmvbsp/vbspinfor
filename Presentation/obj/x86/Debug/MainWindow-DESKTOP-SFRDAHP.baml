   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PresentationPresentation  5   	       Presentation.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,localclr-namespace:Presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   -   5        $��IMPVBSP��5   �     � Title$9  3Chương trình thông tin nhanh VBSP Quảng Bình��6	   $	��661��6>   $
��1241��6K    � WindowStartupLocation$ CenterScreen=�6X    � WindowState$ Normal=�6}    � Icon$ Images/LOGO.bmp��6�   $��20(�6�   $��Normal$�6�   $��Normal&�6�   ���	#FFE10D0D � WindowStyle$ ThreeDBorderWindow=�6   � 	Resources 5       �� 
TargetType �� Property �� Value	 �� Triggers
 P� Property P� Value   	OnLoaded1 W�Name �� 
TargetName �� TargetProperty g� Duration g� RepeatBehavior	 W� By��%4  'm�       &	  v    �� 5   
   " m�6   .�5	      ��  � ��$ 0q�60   �� 5
       � ��
 � � �6+   �� 5       � ��$ Times New Roman)�6+   �� 5       � ��$ 16(�6)   	 5      P� 5      
 �@J�IsMouseOver . . �5      ��  � ��
 �  ��63   �� 5       � ��
 � � �63   �� 5       � ��$
 Bold"�63   5      5      �� 5   
   .��5      W� $ secondAnimation��6   $ secondHandTransform��67   $ Angle�6c   $ 0:1:0D�5      $ Forever��6   $	 360X�65   5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� -   5         ��Name$ OnLoaded1_BeginStoryboard��5      #��[  6A   5      .��5!      � .+�5"   
   o� -   5        $��MainMenu��5"   a   $
��Left=�6   $	��Top=�6*   $	��517��6B    o� 
IsMainMenu . 
�������.@�5$      m� -   5        $��
mnuHeThong��5$   )   $��Hệ Thống��6   $��10"�5%   >    m� Icon 5&      �� 5'      $��Icons/config.ico��6   $��20��66   $��20��6B   .@�5)      m� -   5        $��Import From Oracl To SQL��5)      $
��Left=�6=   $	��270��6X   
���   � 5*      �� 5+      $��Icons/HD-Partitions.ico��6    $��16��6A   $��16��6M   5-      m� 56      -   5        $��mnuUpdateOffline��56   j   $+��%Cập nhật số liệu từ OFFLINE��6   $
��Left=�6A   $
��auto��6\   
���   � 57      �� 58      $��/Icons/Network.ico��6    $��16��6<   $��16��6H   5:      m� 5;      -   5        $��mnuChkUpOffline��5;   k   $"��Check Upload OFFLINE to QBIM��6   $
��Left=�6B   $
��auto��6]   
���   � 5<      �� 5=      $��/Icons/VBSP.ico��6    $��16��69   $��16��6E   5?      m� 5@      -   5        $��mnuPdf��5@   _   $��Read PDF to TEXT��6   $
��Left=�66   $
��auto��6Q   
���   � 5A      �� 5B      $��/Icons/My-Videos.ico��6    $��16��6>   $��16��6J   5D      m� 5F      -	   5        $��mnuExcel��5F   h   $)��#Cập nhật số liệu từ Excel��6   $
��Left=�6?   $
��auto��6Z   
���   � 5G      �� 5H      $$��/Icons/Adminstrative-Tools.ico��6    $��16��6H   $��16��6T   5J      m� 5K      -
   5        $��mnuWord��5K   `   $��Read Word to text��6   $
��Left=�67   $
��auto��6R   
���   � 5L      �� 5M      $"��/Icons/My-Network-Places.ico��6    $��16��6F   $��16��6R   5O      m� 5Q      -   5        $��mnuDbf��5Q   o   $0��*Cập nhật số liệu từ dbf - foxpro��6   $
��Left=�6F   $
��auto��6a   
���   � 5R      �� 5S      $��/Icons/foxpro.png��6    $��16��6;   $��16��6G   5U      m� 5W      -   5        $!��Backup and Restore Database��5W      $
��Left=�6@   $
��auto��6[   
���   � 5X      �� 5Y      $"��/Icons/My-Network-Places.ico��6    $��16��6F   $��16��6R   5[      m� 5\      -   5        $��mnuChangePass��5\   6   $��Đổi mật khẩu USER��6   $
��Left=�6K   $
��auto��6f   
���   � 5]      �� 5^      $��/Icons/ChangePass.ico��6    $��16��6?   $��16��6K   5`      m� 5a      -   5        $'��!Đăng nhập tên khác (LogOut)��5a      $
��Left=�6@   $
��auto��6[   
���   � 5b      �� 5c      $��/Icons/LogOut.jpg��6    $��16��6;   $��16��6G   5e      m� 5f      -   5        $��Xóa dữ liệu  thừa��5f      $
��Left=�66   $
��auto��6Q   $
��auto��6_   
���   � 5g      �� 5h      $��/Icons/delete.ico��6    $��16��6;   $��16��6G   5j      m� 5k      -   5        $��Restart Services��5k      $
��Left=�65   $
��auto��6P   $
��auto��6^   
���   � 5l      �� 5m      $��/Icons/Config-Tools.ico��6    $��16��6A   $��16��6M   5o      m� 5p      -   5        $��mnuUser��5p   Z   $��Quản Trị USER��6   $
��Left=�62   $
��auto��6M   
���   � 5q      �� 5r      $��/Icons/User Computer.png��6    $��16��6B   $��16��6N   5t      m� 5u      -   5        $��	mnuQTCBTD��5u   _   $��Quản Trị CBTD/CBKT��6   $
��Left=�67   $
��auto��6R   
���   � 5v      �� 5w      $��/Icons/Msn.png��6    $��16��68   $��16��6D   5y      m� 5z      -   5        $��mnuChotNguon��5z   `   $ ��Chốt nguồn cuối năm��6   $
��Left=�68   $
��auto��6S   
���   � 5{      �� 5|      $ ��/Icons/Removable-Drive.ico��6    $��16��6D   $��16��6P   5~      m� 5      -   5        $��	mnuLuuPdf��5   \   $��Lưu dữ liệu PDF��6   $
��Left=�64   $
��auto��6O   
���   � 5�      �� 5�      $��/Icons/CSharpAddIn.ico��6    $��16��6@   $��16��6L   5�      m� 5�      -   5        $��Thoát��5�      $
��Left=�6*   $	��140��6E   
���   � 5�      �� 5�      $��/Icons/Exit.jpg��6    $��16��69   $��16��6E   5�      5�      m� 5�      -   5        $��	mnuKeToan��5�   (   $��
Kế Toán��6   $��10"�5�   >    5�      �� 5�      $��Icons/ketoan.jpg��6   $��16��66   $��16��6B   .@�5�      m� -   5        $��Chứng từ phát sinh��5�      
���   � 5�      �� 5�      $��Icons/Search.ico��6    $��16��6:   $��16��6F   5�      m� 5�      -   5        $��	mnuCanDoi��5�   .   $��Cân đối ��6   
���   � 5�      �� 5�      $��Icons/3d bar chart.ico��6    $��16��6@   $��16��6L   5�      m� 5�      -   5        $��mnuCtCanDoi��5�   >   $)��#Một số chỉ tiêu cân đối ��6   
���   � 5�      �� 5�      $��Icons/Calculator.ico��6    $��16��6>   $��16��6J   5�      m� 5�      $��Kế hoạch tài chính��6   
���   � 5�      �� 5�      $��Icons/Favourites.ico��6    $��16��6>   $��16��6J   .@�5�      m� -   5        $��mnuQt14��5�   =   $!��Kết cấu dư nợ (QT14)��6   
���   � 5�      �� 5�      $��Icons/Blog.ico��6$   $��16��6<   $��16��6H   5�      m� 5�      -   5        $.��(Bảng tích số tài khoản (PL05TTL)��5�      
���   � 5�      �� 5�      $��Icons/Globe.ico��6$   $��16��6=   $��16��6I   5�      m� 5�      -   5        $<��6Lãi Tăng/Giảm công trừ ngoại bảng (PL04TL) ��5�      
���   � 5�      �� 5�      $��Icons/Closed-Folder.ico��6$   $��16��6E   $��16��6Q   5�      m� 5�      -   5        $1��+Số dư nguồn vốn huy động (PL03TL)��5�      
���   � 5�      �� 5�      $��Icons/ZIP-File.ico��6$   $��16��6@   $��16��6L   5�      m� 5�      -   5        $7��1Xác định số dư sử dụng vốn (PL02TL) ��5�      
���   � 5�      �� 5�      $��Icons/officemac02.ico��6$   $��16��6C   $��16��6O   5�      m� 5�      -   5        $��	mnukhtc06��5�   B   $$��Dự phòng rủi ro (PL06TL) ��6   
���   � 5�      �� 5�      $��Icons/Risk.ico��6$   $��16��6<   $��16��6H   5�      m� 5�      -    5        $4��.Xác định đơn giá tiền tiền lương ��5�      
���   � 5�      �� 5�      $��Icons/office01.ico��6$   $��16��6@   $��16��6L   5�      m� 5�      -!   5        $-��'Quyết toán kế hoạch tài chính ��5�      
���   � 5�      �� 5�      $��Icons/Blue pin.ico��6$   $��16��6@   $��16��6L   5�      m� 5�      $0��*------------------------------------------��6   
����+��6U   m� 5�      -"   5        $��mnuNVBQ��5�   C   $)��#Số dư nguồn vốn bình quân ��6   
���   � 5�      �� 5�      $��Icons/Blue tag.ico��6$   $��16��6@   $��16��6L   5�      m� 5�      -#   5        $��mnuNVKHB��5�   K   $2��,Số phí tồn ngân bình quân vốn KHB ��6   
���   � 5�      �� 5�      $#��Icons/Adminstrative-Tools.ico��6$   $��16��6K   $��16��6W   5�      m� 5�      -$   5        $��mnuKHTC��5�   =   $��Khoán tài chính 2017��6   
���   � 5�      �� 5�      $��Icons/Bee.ico��6$   $��16��6;   $��16��6G   5�      5�      m� 5�      $��Lương��6   
���   � 5�      �� 5�      $��Icons/cryo_indesign.ico��6    $��16��6A   $��16��6M   .@�5�      m� -%   5        $��Nhập hệ số lương��5�      
���   � 5�      �� 5�      $��Icons/photoshopcs.ico��6$   $��16��6C   $��16��6O   5�      m� 5�      -&   5        $��Tính 20% lương ��5�      
���   � 5�      �� 5�      $��Icons/Closed-Folder.ico��6$   $��16��6E   $��16��6Q   5�      5�      m� 5�      -'   5        $��Điện Báo��5�      
���   � 5�      �� 5�      $��Icons/Search.ico��6    $��16��6:   $��16��6F   5�      m� 5�      -(   5        $��Phân Bổ Lãi KHB��5�      
���   � 5�      �� 5�      $��Icons/Cryoaccess.ico��6    $��16��6>   $��16��6J   5�      m� 5      -)   5        $��mnuTTKU��5   7   $��Thông tin khế ước��6   
���   � 5     �� 5     $��Icons/Connected.ico��6    $��16��6=   $��16��6I   5     m� 5     -*   5        $��mnuTTKH��5  :   $��Thông tin khách hàng��6   
���   � 5     �� 5     $��Icons/Find.ico��6    $��16��68   $��16��6D   5	     m� 5
     -+   5        $��mnuMau06��5
  F   $.��(Kiểm tra Thông tin hồ sơ pháp lý��6   
���   � 5     �� 5     $��Icons/Calendar.ico��6    $��16��6<   $��16��6H   5     5     m� 5     -,   5        $��
mnuTinDung��5  )   $��Tín Dụng��6   $��10"�5  >    5     �� 5     $��Icons/Nt-Services.ico��6   $��16��6;   $��16��6G   .@�5     m� --   5        $��mnuXLN��5  2   $��Kế hoạch XLN��6   
���   � 5     �� 5     $��Icons/Config-Tools.ico��6    $��16��6@   $��16��6L   .@�5     m� -.   5        $��	mnuXLN_M1��5  T   $:��41. Bảng kê NĐH trước 2 tháng (Mẩu 01/NĐH)��6   
���   � 5     �� 5      $ ��Icons/RecycleBin-Empty.ico��6$   $��16��6H   $��16��6T   5"     m� 5#     -/   5        $��	mnuXLN_M3��5#  J   $,��&2. Cập nhật XLN theo Mẩu 01/NĐH��6   
���   � 5$     �� 5%     $ ��Icons/RecycleBin-Empty.ico��6$   $��16��6H   $��16��6T   5'     m� 5(     -0   5        $��	mnuXLN_M4��5(  R   $8��23. In kế hoạch XLN đến hạn Mẩu 02/KHXLN��6   
���   � 5)     �� 5*     $ ��Icons/RecycleBin-Empty.ico��6$   $��16��6H   $��16��6T   5,     5-     m� 5/     -1   5        $��mnuSke��5/  ,   $��Sao Kê��6   
���   � 50     �� 51     $��Icons/Recent-Docs.ico��6    $��16��6?   $��16��6K   .@�53     m� -2   5        $ ��Sao kê khế ước Excel��53     
���   � 54     �� 55     $��Icons/DialUp.ico��6$   $��16��6>   $��16��6J   57     m� 58     -3   5        $��Sao kê khế ước tổ��58     
���   � 59     �� 5:     $��Icons/My-Pictures.ico��6$   $��16��6C   $��16��6O   5<     m� 5K     -4   5        $��Mẩu 01/TG��5K     
���   � 5L     �� 5M     $��Icons/CD.ico��6$   $��16��6:   $��16��6F   5O     m� 5P     -5   5        $*��$Mẩu Kiểm Tra Sử Dung Vốn Vay��5P     
���   � 5Q     �� 5R     $��Icons/Control-Panel.ico��6$   $��16��6E   $��16��6Q   5T     m� 5U     -6   5        $#��Mẩu Đối Chiếu Dư Nợ��5U     
���   � 5V     �� 5W     $��Icons/My Video.ico��6$   $��16��6@   $��16��6L   5Y     m� 5Z     -7   5        $E��?Danh sách đăng kí nộp lãi các chương trình ân hạn��5Z     
���   � 5[     �� 5\     $��Icons/Cryoutlook.ico��6$   $��16��6B   $��16��6N   5^     m� 5_     $0��*------------------------------------------��6   
����+��6V   m� 5`     -8   5        $��	mnuSkeNdh��5`  ;   $��Sao kê nợ đến hạn��6   
���   � 5a     �� 5b     $��Icons/Boss.ico��6$   $��16��6<   $��16��6H   5d     m� 5e     -9   5        $��	mnuSkeNqh��5e  ;   $��Sao kê nợ quá hạn��6   
���   � 5f     �� 5g     $��Icons/Connected.ico��6$   $��16��6A   $��16��6M   5i     m� 5j     -:   5        $��mnuSkeKhoanh��5j  :   $��Sao kê nợ khoanh��6   
���   � 5k     �� 5l     $��Icons/workgroup.ico��6$   $��16��6A   $��16��6M   5n     m� 5o     -;   5        $��
mnuSkePnkt��5o  C   $$��Sao kê phân ngành kinh tế��6   
���   � 5p     �� 5q     $��Icons/Removable-Drive.ico��6$   $��16��6G   $��16��6S   5s     m� 5t     -<   5        $��mnuDongCasa��5t  U   $;��5Danh sách tổ viên ra khỏi tổ- Đóng CASA 105��6   
���   � 5u     �� 5v     $��Icons/Blog.ico��6$   $��16��6<   $��16��6H   5x     m� 5y     -=   5        $��mnuTbGhn��5y  E   $'��!Thông báo gia hạn nợ VB4086��6   
���   � 5z     �� 5{     $��Icons/My Documents1.ico��6$   $��16��6E   $��16��6Q   5}     5�     m� 5�     ->   5        $��
mnuPhatich��5�  7   $��Phân Tích Số Liệu��6   
���   � 5�     �� 5�     $��Icons/My-Videos.ico��6    $��16��6=   $��16��6I   .@�5�     m� -?   5        $��mnuNhapNguon��5�  ;   $��Quản lý nguồn vốn��6   
���   � 5�     �� 5�     $��Icons/Add.ico��6$   $��16��6;   $��16��6G   .@�5�     m� -@   5        $��mnuNguonKHA��5�  ;   $��Nguồn Ủy thác��6#   
���   � 5�     �� 5�  "   $��Icons/IE.ico��6(   $��16��6>   $��16��6J   5�     m� 5�     -A   5        $��
mnuNguonTT��5�  =   $��Nguồn Trực Tiếp��6#   
���   � 5�     �� 5�  "   $��Icons/refresh.gif��6(   $��16��6C   $��16��6O   5�     m� 5�     -B   5        $��mnuNguonCqlv��5�  H   $)��#Nguồn theo cấp quản lý vốn��6#   
���   � 5�     �� 5�  "   $��Icons/Alert.ico��6(   $��16��6A   $��16��6M   5�     m� 5�     -C   5        $��mnuNguonCqlvTinh��5�  C   $!��Nguồn CQLV - Cấp tỉnh��6#   
���   � 5�     �� 5�  "   $��Icons/Connected.ico��6(   $��16��6E   $��16��6Q   5�     m� 5�     -D   5        $)��#Kiểm tra nguồn vốn - dư nợ��5�  #   
���   � 5�     �� 5�  "   $��Icons/Boss.ico��6(   $��16��6@   $��16��6L   5�     m� 5�     -E   5        $��
mnuQlyGqvl��5�  G   $%��Kiểm tra nguồn vốn - GQVL��6#   
���   � 5�     �� 5�  "   $��Icons/Briefcase.ico��6(   $��16��6E   $��16��6Q   5�     5�     m� 5�     -F   5        $��mnuPhanTichDaily��5�  K   $.��(Doanh số Cho vay - Thu nợ - Dư nợ��6   
���   � 5�     �� 5�     $��Icons/Accounting.ico��6$   $��16��6B   $��16��6N   5�     m� 5�     -G   5        $��	mnuUyThac��5�  E   $.��(Số liệu tổ chức hội ủy thác��6   
���   � 5�     �� 5�     $��Icons/Java.ico��6$   $��16��6<   $��16��6H   5�     m� 5�     -H   5        $��MnuCbtd��5�  G   $,��&Số liệu theo cán bộ tín dụng��6   
���   � 5�     �� 5�     $��Icons/SET-File.ico��6$   $��16��6@   $��16��6L   5�     m� 5�     -I   5        $��MnuXato��5�  =   $��Số liệu theo xã tổ��6   
���   � 5�     �� 5�     $��Icons/Briefcase.ico��6$   $��16��6A   $��16��6M   5�     m� 5�     -J   5        $��MnuPnkt��5�  J   $-��'Số liệu theo phân ngành kinh tế��6   
���   � 5�     �� 5�     $��Icons/Run_b.ico��6$   $��16��6=   $��16��6I   5�     m� 5�     -K   5        $��mnuSLVungBien��5�  C   $'��!Số liệu các xã vùng biển��6   
���   � 5�     �� 5�     $��Icons/Java.ico��6$   $��16��6<   $��16��6H   5�     m� 5�     $0��*------------------------------------------��6   
����+��6U   m� 5�     -L   5        $��mnuNqh��5�  D   $&�� Nợ quá hạn theo thời gian��6   
���   � 5�     �� 5�     $��Icons/Blue key.ico��6$   $��16��6@   $��16��6L   5�     m� 5�     -M   5        $��mnuNdhCt��5�  K   $0��*Nợ đến hạn theo chương trình vay��6   
���   � 5�     �� 5�     $��Icons/photoshopcs.ico��6$   $��16��6C   $��16��6O   5�     m� 5�     -N   5        $��mnuTkXa��5�  L   $5��/Số liệu huy động tiết kiệm tại xã��6   
���   � 5�     �� 5�     $��Icons/Connected.ico��6$   $��16��6A   $��16��6M   5�     5�     m� 5�     -O   5        $��KHGNTN��5�  H   $2��,Kế hoạch giải ngân  - thu nợ tuần��6   
���   � 5�     �� 5�     $��Icons/photoshopcs.ico��6    $��16��6?   $��16��6K   .@�5�     m� -P   5        $��Nhập số liệu ��5�     
���   � 5�     �� 5�     $��Icons/workgroup.ico��6$   $��16��6A   $��16��6M   5�     m� 5�     -Q   5        $��mnuInKhGnTn��5�  6   $��In số liệu  ��6   
���   � 5�     �� 5�     $��Icons/Anchor.ico��6$   $��16��6>   $��16��6J   5�     m� 5�     -R   5        $��mnuQtKhGnTn��5�  C   $(��"Quyết toán kế hoạch tuần ��6   
���   � 5�     �� 5�     $��Icons/Anouncement.ico��6$   $��16��6C   $��16��6O   5�     5�     m� 5      -S   5        $��
Lãi Tồn��5      
���   � 5     �� 5     $��Icons/png-image.ico��6    $��16��6=   $��16��6I   5     m� 5     -T   5        $��mnuGiaoNhanTV��5  B   $(��"Chia tách sát nhập tổ VB1670��6   
���   � 5     �� 5     $��Icons/Play-List.ico��6    $��16��6=   $��16��6I   5     m� 5     -U   5        $��mnuHSSV��5  E   $+��%Chương Trình Học Sinh Sinh Viên��6   
���   � 5     �� 5     $��Icons/Config-Tools.ico��6    $��16��6@   $��16��6L   .@�5     m� -V   5        $��
mnuSkeHssv��5  E   $%��Sao kê sinh viên ra trường��6   
���   � 5     �� 5     $ ��Icons/RecycleBin-Empty.ico��6$   $��16��6H   $��16��6T   5     m� 5     -W   5        $2��,Xác định ngày đến hạn cuối cùng��5     
���   � 5     �� 5     $��Icons/IE.ico��6$   $��16��6:   $��16��6F   5     5      m� 5!     -X   5        $��mnuChamDiemGDX��5!  <   $%��Chấm điểm GDX kiểu mẩu��6   
���   � 5"     �� 5#     $��Icons/UBND.ico��6    $��16��68   $��16��6D   5%     m� 5&     -Y   5        $��	mnuTLGDXA��5&  8   $��Tỷ lệ giao dịch xã��6   
���   � 5'     �� 5(     $��Icons/Blue pin.ico��6    $��16��6<   $��16��6H   5*     m� 5+     -Z   5        $��mnuTLNQH��5+  6   $��Tỷ lệ nợ quá hạn��6   
���   � 5,     �� 5-     $��Icons/BMP-Image.ico��6    $��16��6=   $��16��6I   5/     m� 50     -[   5        $��mnuKSDT��50  7   $��Khảo sát điều tra��6   
���   � 51     �� 52     $��Icons/Msn.png��6    $��16��67   $��16��6C   54     m� 55     -\   5        $��mnuKHTD��55  M   $=��7Kết quả thực hiện kế hoạch tín dụng năm��6   
���   � 56     �� 57     $��Icons/Cryoutlook.ico��6    $��16��6>   $��16��6J   59     5;     m� 5>     -]   5        $��mnuKiemSoat��5>  *   $��Kiểm Soát��6   $��10"�5?  >    5@     �� 5A     $��Icons/Close Folder.ico��6   $��16��6<   $��16��6H   .@�5C     m� -^   5        $��Chương Trình KT740��5C     $
��Left=�67   $	��270��6R   
���   � 5D     �� 5E     $��Icons/HD-Partitions.ico��6    $��16��6A   $��16��6M   5G     m� 5H     -_   5        $0��*Danh sách hộ nghèo - hộ cận nghèo��5H     $
��Left=�6F   $	��370��6a   
���   � 5I     �� 5J     $��Icons/HD-Partitions.ico��6    $��16��6A   $��16��6M   5L     m� 5N     -`   5        $��Số liệu D15,D20 NHNN��5N     $
��Left=�69   $	��370��6T   
���   � 5O     �� 5P     $��Icons/Connected.ico��6    $��16��6=   $��16��6I   5R     m� 5Z     -a   5        $��mnuNHNN��5Z  x   $=��7Số liệu theo loại hình, nghành nghề kinh tế��6   $
��Left=�6P   $	��370��6k   
���   � 5[     �� 5\     $��Icons/Run_b.ico��6    $��16��69   $��16��6E   5^     m� 5_     -b   5        $��mnuKTHDT��5_  y   $?��9Theo dõi công tác kiểm tra của hội đoàn thể ��6   $
��Left=�6Q   $	��370��6l   
���   � 5`     �� 5a     $��Icons/Desktop1.ico��6    $��16��6<   $��16��6H   5c     m� 5e     -c   5        $��
mnuTienIch��5e  U   $��Tiện ích��6   $
��Left=�6-   $	��370��6H   
���   � 5f     �� 5g     $��Icons/1977-10164.png��6    $��16��6>   $��16��6J   5i     5k     m� 5n     -d   5        $��mnuOther��5n  &   $��Other��6   $��10"�5o  >    5p     �� 5q     $��Icons/Help.ico��6   $��16��64   $��16��6@   .@�5s     m� -e   5        $��mnuHelp��5s  Q   $
��Help��6   $
��Left=�6)   $
��auto��6D   
���   � 5t     �� 5u     $��Icons/Help.ico��6    $��16��68   $��16��6D   5w     m� 5x     -f   5        $��mnuTracNghiem��5x  5   $��Thi trắc nghiệm��6   
���   �$��10"�5y  B    5z     �� 5{     $��Icons/Blue pin.ico��6    $��16��6<   $��16��6H   .@�5}     m� -g   5        $��mnuNhapCHTN��5}  :   $��1. Nhập câu hỏi��6   
���   � 5~     �� 5     $��Icons/Brush.ico��6$   $��16��6=   $��16��6I   5�     m� 5�     -h   5        $��mnuThiTN��5�  =   $��2. Thi trắc nghiệm��6   
���   � 5�     �� 5�     $��Icons/Bad mark.ico��6$   $��16��6@   $��16��6L   5�     m� 5�     -i   5        $��	mnuBaoCao��5�  5   $��3. Báo Cáo��6   
���   � 5�     �� 5�     $��Icons/My Video.ico��6$   $��16��6@   $��16��6L   5�     m� 5�     -j   5        $��
mnuDanhGia��5�  C   $$��4. Đánh giá sau khóa học��6   
���   � 5�     �� 5�     $��Icons/Control-Panel.ico��6$   $��16��6E   $��16��6Q   5�     5�     m� 5�     -k   5        $��mnuThiNV��5�  8   $��Thi nghiệp vụ giỏi��6   
���   �$��10"�5�  B    5�     �� 5�     $��Icons/Cryoaccess.ico��6    $��16��6>   $��16��6J   .@�5�     m� -l   5        $��	mnuNhapCH��5�  :   $��1. Nhập câu hỏi��6   
���   � 5�     �� 5�     $��Icons/Blue tag.ico��6$   $��16��6@   $��16��6L   5�     m� 5�     -m   5        $��mnuLuyenThi��5�  7   $��2. Luyện thi��6   
���   � 5�     �� 5�     $��Icons/Default_b.ico��6$   $��16��6A   $��16��6M   5�     m� 5�     -n   5        $��mnuThi��5�  <   $��3. Thi chính thức��6   
���   � 5�     �� 5�     $��Icons/Abort.ico��6$   $��16��6=   $��16��6I   5�     5�     m� 5�     -o   5        $��mnuHscb��5�  W   $��Điện Thoại��6   $
��Left=�6/   $
��auto��6J   
���   � 5�     �� 5�     $��Icons/Telephone.ico��6    $��16��6=   $��16��6I   5�     m� 5�     -p   5        $��mnuAbout��5�  R   $��About��6   $
��Left=�6*   $
��auto��6E   
���   � 5�     �� 5�     $��Icons/About.ico��6    $��16��69   $��16��6E   5�     m� 5�     -q   5        $��Nhật ký hỗ trợ��5�     $
��Left=�63   $
��auto��6N   
���   � 5�     �� 5�     $"��/Icons/My-Network-Places.ico��6    $��16��6F   $��16��6R   5�     m� 5�     -r   5        $
��TEST��5�     $
��Left=�6)   $
��auto��6D   
���   � 5�     �� 5�     $��/Icons/Alien.ico��6    $��16��6:   $��16��6F   5�     m� 5�     -s   5        $��	mnuVPPham��5�  4   $��Văn phòng phẩm��6   
���   �$��10"�5�  B    5�     �� 5�     $��Icons/My Computer.ico��6    $��16��6?   $��16��6K   .@�5�     m� -t   5        $��	mnuDonGia��5�  I   $-��'1. Cập nhật danh mục - đơn giá��6   
���   � 5�     �� 5�     $��Icons/cryo_golive.ico��6$   $��16��6C   $��16��6O   5�     m� 5�     -u   5        $��
mnuGiayVpp��5�  ?   $��2. Giấy xin nhận VPP��6   
���   � 5�     �� 5�     $��Icons/Control-Panel.ico��6$   $��16��6E   $��16��6Q   5�     m� 5�     -v   5        $��mnuBaoCaoVpp��5�  S   $9��33. Dự toán - tổng hợp- biên bản bàn giao��6   
���   � 5�     �� 5�     $��Icons/Floppy.ico��6$   $��16��6>   $��16��6J   5�     5�     5�     5�     �� 5�  
   -w   5        $��imgMain1��5�  M   $	��283��6   $
��Left=�6   $��381,136,0,0q�68   $
��Fill=�6]   $	��Top=�6l   $	��439��6�   $��Images/NetWork.jpg��6�   �� 5�  
   -x   5        $��txtUserName��5�     $
��auto��6'   $��13(�64   $
��Bold"�6B   
����iA�$��173,588,47,10q�6l   $��32��6�   A� 5�  
   -y   5         A�Name$ Circle1��5�     $��892,48,90,371q�6#   ��5�     
� 5�      
� GradientOrigin$ 0.475,0.506&�6&   .��5�     �  � Color$ 	#FFF9F9FD��6#    � Offset$ 0Q�65   � 5�     $ White��6#   $ 1Q�61   � 5�     $ White��6#   5�     5�     A� 5�  
   -z   5        $ Circle2��5�     $��898,53,96,377q�6#   ��5�     
� 5�     $ 0.475,0.506&�6&   .��5�     � $ 	#FFF9F9FD��6#   $ 0Q�65   � 5�     $ White��6#   $ 1Q�61   � 5�     $ White��6#   5�     5�     �� 5�  
   -{   5         ��Name$ Label12��5�     $��12��6!   $��993,51,194,0q�6.   $
��Bold"�6D   $��Italic$�6V   $��21.333(�6i   $
��auto��6�   $	��Top=�6�   ��6{   Z� ��5�     �� 5�      �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5�     � $ Black��6#   $ 0Q�61   � 5�     $ 	#FFF31313��6#   $ 1Q�65   5�     5�     �� 5   
   -|   5        $ Label6��5      $��6��6    $��1006,213,181,0q�6,   $
��Bold"�6D   $��Italic$�6V   $��21.333(�6i   $
��auto��6�   $	��Top=�6�   ��6{   Z� ��5     �� 5     $ 0.5,1&�6&   $ 0.5,0&�67   .��5     � $ Black��6#   $ 0Q�61   � 5     $ 	#FFF31313��6#   $ 1Q�65   5     5     �� 5	  
   -}   5        $ Label9��5	     $��9��6    $��905,126,282,0q�6,   $
��Bold"�6C   $��Italic$�6U   $��21.333(�6h   $
��auto��6�   $	��Top=�6�   ��6z   Z� ��5
     �� 5     $ 0.5,1&�6&   $ 0.5,0&�67   .��5     � $ Black��6#   $ 0Q�61   � 5     $ 	#FFF31313��6#   $ 1Q�65   5     5     �� 5  
   -~   5        $ Label3��5     $��3��6    $��1105,126,82,0q�6,   $
��Bold"�6C   $��Italic$�6U   $��21.333(�6h   $
��auto��6�   $	��Top=�6�   ��6z   Z� ��5     �� 5     $ 0.5,1&�6&   $ 0.5,0&�67   .��5     � $ Black��6#   $ 0Q�61   � 5     $ 	#FFF31313��6#   $ 1Q�65   5     5     �� 5  
   -   5        $ 	Labeldate��5     $�� ��6#   $��979,170,168,0q�6.   $
��Bold"�6E   $��Normal$�6W   $��12(�6k   $
��auto��6�   $	��Top=�6�   ��6y   Z� ��5     �� 5     $ 0.5,1&�6&   $ 0.5,0&�67   .��5     � $ Green��6#   $ 0Q�61   � 5     $ Green��6#   $ 1Q�61   5     5      �� 5#  
   -�   5         ��Name$ 
Minutehand��5#     $��2��6(   $��66��62   $��1012,86,208,0q�6>   $	��Top=�6U    J� RenderTransformOrigin$ 0.5,1&�6m   ��5$     �� 5%     $ 0.5,1&�6&     � MappingMode$  RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5&     � $ 	#FFE42C5F��6#   $ 1Q�65   � 5'     $ 	#FFE42C5F��6#   5(     5*     �� 5+  
   -�   5        $ Hourhand��5+     $��2��6&   $��48��60   $��1012,101,208,0q�6<   $	��Top=�6T   $ 0.5,1&�6l   ��5,     �� 5-     $ 0.5,1&�6&   $  RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5.     � $ 	#FF1F0091��6#   $ 1Q�65   � 5/     $ 	#FF1F0091��6#   50     52     �� 53  
   -�   5        $ 
Secondhand��53     $��83��6(   $��1016,68,0,0q�64   $	��Top=�6I   $
��Left=�6a   $��2��6|   $ 0.5,1&�6�   z�54     V� 55     .~�56     �� -�   5        secondHandTransform��56  &   57     ��59     �� 5:     $ 0.5,1&�6&   $  RelativeToBoundingBox=�67   $ 0.5,0&�6[   .��5;     � $ 	#FF100F0F��6#   $ 0.996Q�65   � 5<     $ Black��6#   5=     5?     �� 5@  
   -�   5        ! ��Name$! imgMain��5@  L   $��88��6   $
��Left=�6   $��1082,22,0,0q�67   $
��Fill=�6]   $	��Top=�6l   $	��104��6�   $-��'/Presentation;component/Images/LOGO.bmp��6�   5B     5C     