   M S B A M L   `   `   `  ����L  HStitching Pattern, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Stitching_PatternStitching_Pattern  5   	   "    Stitching_Pattern.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Stitching_Pattern     5   	     � Title$  
Random Art��5   	   $	��800��6   $	��650��6)    � 
ResizeMode$ NoResize=�65    � AllowsTransparency .  � WindowStyle$
 None=�6e   
������  � Topmost .  � 	Resources 5	      .   (Stitching_Pattern.InverseDoubleConverter   convert��%   &	          5
   
   6'   5      .��5      � 
������ .+�5   
   �� $y�Hidden=�6   .+�5      �� $��Settings��6   $
��Bold"�6'   �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $��Speed��6   �� 5      -   5         ��Name$ Slider_Speed��5      $	��100��6/    � Minimum$ 1Q�6;   	 � Maximum$		 100Q�6G   
 �� TickFrequency$
 1Q�6U    �� IsSnapToTickEnabled . 6g   �� 5      ��6   ��  �� ElementName$ Slider_Speed�� �� Path$ Value�5      �� 5      $ 
Horizontal=�6   .+�5      �� $��
Odds Ratio��6   �� 5      $��30��6u   ��6   �� $ Slider_Ratio��$ Value� �� 	Converter# [  6u   �� 5      -   5        $ Slider_Ratio��5      $	��100��6/   $ 0Q�6;   $		 100Q�6G   $
 5Q�6U    .  � Value$ 50Q�6�   �� 5      ��6   �� $ Slider_Ratio��$ Value��� 5      -   5        $��New Seed��5      $��80��6A   $��20,0,0,0q�6L   5      5      �� 5   
   -   5         ��Name$ 	PrintArea��5      $
��1000��6%   $��	-50,0,0,0q�63   �� 5   
   -   5        $��Right=�5      $	��Top=�6-   $	��200��6E   $	��100��6Q   
������� J� Opacity$
 0.01Q�6q    �� CornerRadius$ 0,0,0,10��6�    J� IsHitTestVisible . ._�5!      � .+�5"      �� $��Click and drag to move��6   $	��Top=�69   $��Center=�6Q   �� 5#      -   5        $��X��5#      $��80��6%   $��40��60   $��28(�6S   5$      5%      5&      5'      