   M S B A M L   `   `   `  ����  HelpDesk9    3HelpDesk.ViewAltas.Catálogo.hdk_ViewAltaTipoEquipo   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$)  #Help Desk - Alta de tipos de equipo��5      $��429.018��63   $��450.805��6D   $��13(�6T    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.    � WindowStyle$
 None=�5   	    � Icon$3 -/HelpDesk;component/Imagenes/diseno_icono.png��6    � AllowsTransparency . 
������ .��5	      �� $��10q�6   
���  ��$	��0.5q�6+    �� CornerRadius$ 15��6A   
������� J� Effect 5
   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6   	   	Direction$		 320Q�5      
   
BlurRadius$
 15Q�5         ShadowDepth$ 3Q�5      ._�5   
   ]� ��. 
������ $��-2,0,2,0q�6B   .+�5      � $	��Top=�6   $��25��6(   .+�5      �� $��Center=�6   $��Center=�5      ��5   7   ��  �� Path$ Title� �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ WindowL�5      �� 5      $%��../../Imagenes/diseno_icono.png��6   $��Bottom=�6A   $��20��6\   $��20��6g   $��	9,0,401,1q�6s   �� 5      -   5        $��titleBar��5      
������ 5      �� 5      -   5        $��closeButton��5         CerrarStyle#��[  5      $��Right=�5      $��15��5      $��0,0,5,0q�6    $��15��61   5      �� 5      -   5        $��	minButton��5        MinimizarStyle#��[ 5      $��Right=�5      $��15��5       $��0,0,25,0q�6    $��9��62   5!      5"      � 5$      .+�5%      �� $��Nombre ��6   $
��Left=�6*   $��	47,14,0,0q�6E   $��16(�6X   $	��Top=�6f   �� 5&      -   5        $��txtTipoEquipo��5&   {   $��127,0,0,337q�6    �� 	MaxLength$ 30��6/   $��Center=�6>   $
��Left=�6`   $��31��6�    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $��Bottom=�6�   $	��253��6�   6�   �� 5'      
���   �$!��Características a capturar��6/   $��15(�6S   $
��Left=�6a   $��	49,64,0,0q�6|   $	��Top=�6�   $	��248��6�   $	��333��6�   .��5(      � .+�5)      �� -   5         ��Name$ equipo��5)   #   $��Marca del equipo��63   $
��Left=�6N   $��	14,10,0,0q�6i   $	��Top=�6|   {�.   d� 	IsChecked$
 TrueY�6�   �� 5*      -   5        $��dd��5*   #   $!��Capacidad de almacenamiento��6-   $
��Left=�6S   $��	14,32,0,0q�6n   $	��Top=�6�   �� 5+      -   5        $ 
procesador��5+   #   $(��"Velocidad del reloj del procesador��67   $
��Left=�6d   $��	14,56,0,0q�6   $	��Top=�6�   �� 5,      -   5        $	 ram��5,   #   $��Capacidad de RAM��60   $
��Left=�6K   $��	14,80,0,0q�6f   $	��Top=�6y   �� 5-      -	   5        $	��red��5-   #   $��Direcciones de red��6.   $
��Left=�6K   $��
14,102,0,0q�6f   $	��Top=�6z   �� 5.      -
   5        $��monitor��5.   #   $��Marca del Monitor��62   $
��Left=�6N   $��
14,125,0,0q�6i   $	��Top=�6}   �� 5/      -   5        $��teclado��5/   #   $��Marca del teclado��62   $
��Left=�6N   $��
14,149,0,0q�6i   $	��Top=�6}   �� 50      -   5        $��mouse��50   #   $��Marca del mouse��60   $
��Left=�6J   $��
14,172,0,0q�6e   $	��Top=�6y   �� 51      -   5        $ chSeleccionar��51   #   $,��&Seleccionar todas las características��6:   $
��Left=�6j   $��
14,195,0,0q�6�   $	��Top=�6�   6�   56      57      �� 58      -   5        $��
btnAñadir��58   c     BotonAceptarStyle#��[ 6   $��16(�6D   $��Añadir��6R   $
��Left=�6t   $��182,324,0,0q�6�   $	��Top=�6�   $��75��6�    J� RenderTransformOrigin$ 0.48,-0.423&�6�    �� 	IsDefault . 6  59      5:      5;      5<      