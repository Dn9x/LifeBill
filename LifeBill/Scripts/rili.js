/*
 * jQuery插件
 *
 */

 (function ($) {
     //默认值
     var defaults = {
         year: new Date().getFullYear(),
         month: new Date().getMonth() + 1,
         url: null,
         data: null
     };

     //获取指定月份有多少天
     function daysInMonth(year, month) {
         month = parseInt(month, 10);  //parseInt(number,type)这个函数后面如果不跟第2个参数来表示进制的话，默认是10进制。
         var temp = new Date(year, month, 0);
         return temp.getDate();
     }

     function setUrl(year, month) {
         var yu, mu, yd, md;

         if (month == 1) {
             yd = year - 1;
             md = 12;
             yu = year;
             mu = month + 1;
         }
         else if (month == 12) {
             yd = year;
             md = month - 1;
             yu = year + 1;
             mu = 1;
         }
         else {
             yd = year;
             md = month - 1;
             yu = year;
             mu = month + 1;
         }

         var down = "javascript:Up(" + yd + "," + md + ")";
         var up = "javascript:Up(" + yu + "," + mu + ")";

         var div = '<div style="margin:2px auto; border:1px solid #ccc; width:90%; min-width:812px; height:26px;">' +
                   '<div style="width:50px; height:22px; line-height:22px; float:left; text-align:center; margin:2px auto;">' +
                   '<a id="rili_down" style="color:#5c87b2;" href=' + down + '>后退</a>' +
                   '</div>' +
                   '<div style="width:100px; height:22px; line-height:22px; float:left; text-align:center; margin:2px auto;">' + year + '/' + month + '</div>' +
                   '<div style="width:50px; height:22px; line-height:22px; float:left; text-align:center; margin:2px auto;">' +
                   '<a id="rili_up" style="color:#5c87b2;" href=' + up + '>前进</a>' +
                   '</div>' +
                   '<div style="width:200px; height:22px; line-height:22px; float:right; text-align:center; margin:2px auto;">' +
                   '<embed src="/Content/free-flash-clock.swf" width="100" height="22" wmode="transparent" type="application/x-shockwave-flash"></embed>' +
                   '</div>' +
                   '</div>';

         return div;
     }

     $.ajaxSetup({
         async: false
     });

     $.fn.showrili = function (options) {
         //合并属性
         options = $.extend(defaults, options);

         $.post(
			options.url,
			{ "y": defaults.year, "m": defaults.month },
			function (json, textStatus) {
			    if (textStatus = "success") {
			        var dats = eval('(' + json + ')');

			        defaults.data = dats;
			    }
			}
		 );

         //合并属性
         options = $.extend(defaults, options);

         //拼接当前时间
         var dq = options.year + '-' + options.month + '-' + '1';

         //获取当前时间对象
         var d = new Date(dq);

         //获取当前月的第一天是礼拜几以数字表示
         var week = d.getDay() == 0 ? 7 : d.getDay();

         //拼接table头部
         var tab = setUrl(options.year, options.month) + '<div style="margin:10px auto; border:1px solid #ccc; width:90%; min-width:812px; height:auto;">' +
                   '<table style="width:100%" cellspacing="0">' +
			      '<thead>' +
			      '<tr>' +
			      '<th style="text-align:center;">星期一</th>' +
			      '<th style="text-align:center;">星期二</th>' +
			      '<th style="text-align:center;">星期三</th>' +
			      '<th style="text-align:center;">星期四</th>' +
			      '<th style="text-align:center;">星期五</th>' +
			      '<th style="text-align:center;">星期六</th>' +
			      '<th style="text-align:center;">星期天</th>' +
			      '</tr>' +
			      '</thead>' +
			      '<tbody>' +
			      '<tr>';

         //1.1.判断当前月的日历中从一号开始的前面有几个空格
         var qg = week - 1;

         //1.2.获取当月1号前面的日期是上个月的那几天：上个月天数-空格数+1
         var qm = 0;
         if (options.month == 1) {	//如果是1月那么年份要-1
             qm = daysInMonth(options.year - 1, 12) - qg + 1;
         } else {
             qm = daysInMonth(options.year, options.month - 1) - qg + 1;
         }

         //1.3.循环添加前面的空格数目
         for (var i = 0; i < qg; i++) {
             tab += "<td>" +
                   "<div class='td_div_main'>" +
                   "<div class='td_div_topg'>" + (qm + i) + "</div>" +
                   "<div class='td_div_cont'>&nbsp;</div>" +
                   "</div>" +
                   "</td>";
         }

         //1.4.循环这一行里面剩下的几天
         for (var i = 0; i < (7 - week + 1); i++) {
             var name = eval("options.data._" + options.year + "" + options.month + "" + (i + 1) + " == undefined ? '0/0' : options.data._" + options.year + "" + options.month + "" + (i + 1) + "[0].total");
             var z = eval("options.data._" + options.year + "" + options.month + "" + (i + 1) + " == undefined ? '0' : options.data._" + options.year + "" + options.month + "" + (i + 1) + "[0].id");
             var url = eval("options.data._" + options.year + "" + options.month + "" + (i + 1) + " == undefined ? 'Add?y="+options.year+"&m="+options.month+"&d="+(i+1)+"' : 'Upd?i=" + z + "'");

             tab += "<td>" +
                   "<div class='td_div_main'>" +
                   "<div class='td_div_top'>" + (1 + i) + "</div>" +
                   "<div class='td_div_cont'><a href='/Home/" + url + "'>" + name + "</a></div>" +
                   "</div>" +
                   "</td>";
         }

         //1.5.第一行数据拼接完成
         tab += "</tr>";

         //2.1.计算本月有多少天
         var dd = daysInMonth(options.year, options.month);

         //2.2.计算还有多少天要循环
         var sd = dd - (7 - week + 1);

         //2.3.计算整行的循环还有多少行
         var hd = Math.floor(sd / 7);

         //累加余下的日期开始数
         var lc = 7 - week + 1;

         //2.5.开始循环整行的
         for (var i = 0; i < hd; i++) {
             tab += "<tr>";

             //2.5.1.开始循环整行里面的td
             for (var j = 0; j < 7; j++) {
                 lc++;
                 var name = eval("options.data._" + options.year + "" + options.month + "" + lc + " == undefined ? '0/0' : options.data._" + options.year + "" + options.month + "" + lc + "[0].total");
                 var z = eval("options.data._" + options.year + "" + options.month + "" + lc + " == undefined ? '0' : options.data._" + options.year + "" + options.month + "" + lc + "[0].id");
                 var url = eval("options.data._" + options.year + "" + options.month + "" + lc + " == undefined ? 'Add?y=" + options.year + "&m=" + options.month + "&d=" + lc + "' : 'Upd?i=" + z + "'");
                 tab += "<td>" +
                       "<div class='td_div_main'>" +
                       "<div class='td_div_top'>" + lc + "</div>" +
                       "<div class='td_div_cont'><a href='/Home/" + url + "'>" + name + "</a></div>" +
                       "</div>" +
                       "</td>";
             }

             tab += "</tr>";
         }

         //3.1.计算整行之后还有多少天剩余要循环 options.data._20131011[0].id
         var gd = sd % 7;

         //3.2.循环剩余没有循环的td
         tab += "<tr>";
         for (var i = 0; i < gd; i++) {
             lc++;
             var name = eval("options.data._" + options.year + "" + options.month + "" + lc + " == undefined ? '0/0' : options.data._" + options.year + "" + options.month + "" + lc + "[0].total");
             var z = eval("options.data._" + options.year + "" + options.month + "" + lc + " == undefined ? '0' : options.data._" + options.year + "" + options.month + "" + lc + "[0].id");
             var url = eval("options.data._" + options.year + "" + options.month + "" + lc + " == undefined ? 'Add?y=" + options.year + "&m=" + options.month + "&d=" + lc + "' : 'Upd?i=" + z + "'");
             tab += "<td>" +
                   "<div class='td_div_main'>" +
                   "<div class='td_div_top'>" + lc + "</div>" +
                   "<div class='td_div_cont'><a href='/Home/" + url + "'>" + name + "</a></div>" +
                   "</div>" +
                   "</td>";
         }

         //3.3.循环最后一行不够循环剩余的td个数
         for (var i = 0; i < 7 - gd; i++) {
             tab += "<td>" +
                   "<div class='td_div_main'>" +
                   "<div class='td_div_topg'>" + (i + 1) + "</div>" +
                   "<div class='td_div_cont'>&nbsp;</div>" +
                   "</div>" +
                   "</td>";
         }

         //3.4.拼接完成
         tab += "</tr>" +
        	   "</tbody>" +
			   "</table></div>";


         $(this).html(tab);
     }


 })(jQuery);