var Lobibox=Lobibox||{};(function(){Lobibox.counter=0;Lobibox.prompt=function(type,options){return new LobiboxPrompt(type,options);};Lobibox.confirm=function(options){return new LobiboxConfirm(options);};Lobibox.progress=function(options){return new LobiboxProgress(options);};Lobibox.error={};Lobibox.success={};Lobibox.warning={};Lobibox.info={};Lobibox.alert=function(type,options){if(["success","error","warning","info"].indexOf(type)>-1){return new LobiboxAlert(type,options);}};Lobibox.window=function(options){return new LobiboxWindow('window',options);};var LobiboxBase={$type:null,$el:null,$options:null,debug:function(){if(this.$options.debug){window.console.debug.apply(window.console,arguments);}},_processInput:function(options){if($.isArray(options.buttons)){var btns={};for(var i=0;i<options.buttons.length;i++){btns[options.buttons[i]]=Lobibox.base.OPTIONS.buttons[options.buttons[i]];}
options.buttons=btns;}
options.customBtnClass=options.customBtnClass?options.customBtnClass:Lobibox.base.DEFAULTS.customBtnClass;for(var i in options.buttons){if(options.buttons.hasOwnProperty(i)){var btn=options.buttons[i];btn=$.extend({},Lobibox.base.OPTIONS.buttons[i],btn);if(!btn['class']){btn['class']=options.customBtnClass;}
options.buttons[i]=btn;}}
options=$.extend({},Lobibox.base.DEFAULTS,options);if(options.showClass===undefined){options.showClass=Lobibox.base.OPTIONS.showClass;}
if(options.hideClass===undefined){options.hideClass=Lobibox.base.OPTIONS.hideClass;}
if(options.baseClass===undefined){options.baseClass=Lobibox.base.OPTIONS.baseClass;}
if(options.delayToRemove===undefined){options.delayToRemove=Lobibox.base.OPTIONS.delayToRemove;}
if(!options.iconClass){options.iconClass=Lobibox.base.OPTIONS.icons[options.iconSource][this.$type];}
return options;},_init:function(){var me=this;me._createMarkup();me.setTitle(me.$options.title);if(me.$options.draggable&&!me._isMobileScreen()){me.$el.addClass('draggable');me._enableDrag();}
if(me.$options.closeButton){me._addCloseButton();}
if(me.$options.closeOnEsc){$(document).on('keyup.lobibox',function(ev){if(ev.which===27){me.destroy();}});}
if(me.$options.baseClass){me.$el.addClass(me.$options.baseClass);}
if(me.$options.showClass){me.$el.removeClass(me.$options.hideClass);me.$el.addClass(me.$options.showClass);}
me.$el.data('lobibox',me);},_calculatePosition:function(position){var me=this;var top;if(position==='top'){top=30;}else if(position==='bottom'){top=$(window).outerHeight()-me.$el.outerHeight()-30;}else{top=($(window).outerHeight()-me.$el.outerHeight())/2;}
var left=($(window).outerWidth()-me.$el.outerWidth())/2;return{left:left,top:top};},_createButton:function(type,op){var me=this;var btn=$('<button></button>').addClass(op['class']).attr('data-type',type).html(op.text);if(me.$options.callback&&typeof me.$options.callback==='function'){btn.on('click.lobibox',function(ev){var bt=$(this);me._onButtonClick(me.$options.buttons[type],type);me.$options.callback(me,bt.data('type'),ev);});}
btn.click(function(){me._onButtonClick(me.$options.buttons[type],type);});return btn;},_onButtonClick:function(buttonOptions,type){var me=this;if((type==='ok'&&me.$type==='prompt'&&me.isValid()||me.$type!=='prompt'||type!=='ok')&&buttonOptions&&buttonOptions.closeOnClick){me.destroy();}},_generateButtons:function(){var me=this;var btns=[];for(var i in me.$options.buttons){if(me.$options.buttons.hasOwnProperty(i)){var op=me.$options.buttons[i];var btn=me._createButton(i,op);btns.push(btn);}}
return btns;},_createMarkup:function(){var me=this;var lobibox=$('<div class="lobibox"></div>');lobibox.attr('data-is-modal',me.$options.modal);var header=$('<div class="lobibox-header"></div>').append('<span class="lobibox-title"></span>');var body=$('<div class="lobibox-body"></div>');lobibox.append(header);lobibox.append(body);if(me.$options.buttons&&!$.isEmptyObject(me.$options.buttons)){var footer=$('<div class="lobibox-footer"></div>');footer.append(me._generateButtons());lobibox.append(footer);if(Lobibox.base.OPTIONS.buttonsAlign.indexOf(me.$options.buttonsAlign)>-1){footer.addClass('text-'+me.$options.buttonsAlign);}}
me.$el=lobibox.addClass(Lobibox.base.OPTIONS.modalClasses[me.$type]);},_setSize:function(){var me=this;me.setWidth(me.$options.width);if(me.$options.height==='auto'){me.setHeight(me.$el.outerHeight());}else{me.setHeight(me.$options.height);}},_calculateBodyHeight:function(height){var me=this;var headerHeight=me.$el.find('.lobibox-header').outerHeight();var footerHeight=me.$el.find('.lobibox-footer').outerHeight();return height-(headerHeight?headerHeight:0)-(footerHeight?footerHeight:0);},_addBackdrop:function(){if($('.lobibox-backdrop').length===0){$('body').append('<div class="lobibox-backdrop"></div>');}},_triggerEvent:function(type){var me=this;if(me.$options[type]&&typeof me.$options[type]==='function'){me.$options[type](me);}},_calculateWidth:function(width){var me=this;width=Math.min(Math.max(width,me.$options.width),$(window).outerWidth());if(width===$(window).outerWidth()){width-=2*me.$options.horizontalOffset;}
return width;},_calculateHeight:function(height){var me=this;console.log(me.$options.height);height=Math.min(Math.max(height,me.$options.height),$(window).outerHeight());if(height===$(window).outerHeight()){height-=2*me.$options.verticalOffset;}
return height;},_addCloseButton:function(){var me=this;var closeBtn=$('<span class="btn-close">&times;</span>');me.$el.find('.lobibox-header').append(closeBtn);closeBtn.on('mousedown',function(ev){ev.stopPropagation();});closeBtn.on('click.lobibox',function(){me.destroy();});},_position:function(){var me=this;me._setSize();var pos=me._calculatePosition();me.setPosition(pos.left,pos.top);},_isMobileScreen:function(){return $(window).outerWidth()<768;},_enableDrag:function(){var el=this.$el,heading=el.find('.lobibox-header');heading.on('mousedown.lobibox',function(ev){el.attr('offset-left',ev.offsetX);el.attr('offset-top',ev.offsetY);el.attr('allow-drag','true');});$(document).on('mouseup.lobibox',function(){el.attr('allow-drag','false');});$(document).on('mousemove.lobibox',function(ev){if(el.attr('allow-drag')==='true'){var left=ev.clientX-parseInt(el.attr('offset-left'),10)-parseInt(el.css('border-left-width'),10);var top=ev.clientY-parseInt(el.attr('offset-top'),10)-parseInt(el.css('border-top-width'),10);el.css({left:left,top:top});}});},_setContent:function(msg){var me=this;me.$el.find('.lobibox-body').html(msg);return me;},_beforeShow:function(){var me=this;me._triggerEvent('onShow');},_afterShow:function(){var me=this;Lobibox.counter++;me.$el.attr('data-nth',Lobibox.counter);if(!me.$options.draggable){$(window).on('resize.lobibox-'+me.$el.attr('data-nth'),function(){me.refreshWidth();me.refreshHeight();me.$el.css('left','50%').css('margin-left','-'+(me.$el.width()/2)+'px');me.$el.css('top','50%').css('margin-top','-'+(me.$el.height()/2)+'px');});}
me._triggerEvent('shown');},_beforeClose:function(){var me=this;me._triggerEvent('beforeClose');},_afterClose:function(){var me=this;if(!me.$options.draggable){$(window).off('resize.lobibox-'+me.$el.attr('data-nth'));}
me._triggerEvent('closed');},hide:function(){var me=this;if(me.$options.hideClass){me.$el.removeClass(me.$options.showClass);me.$el.addClass(me.$options.hideClass);setTimeout(function(){callback();},me.$options.delayToRemove);}else{callback();}
function callback(){me.$el.addClass('lobibox-hidden');if($('.lobibox[data-is-modal=true]:not(.lobibox-hidden)').length===0){$('.lobibox-backdrop').remove();$('body').removeClass(Lobibox.base.OPTIONS.bodyClass);}}
return this;},destroy:function(){var me=this;me._beforeClose();if(me.$options.hideClass){me.$el.removeClass(me.$options.showClass).addClass(me.$options.hideClass);setTimeout(function(){callback();},me.$options.delayToRemove);}else{callback();}
function callback(){me.$el.remove();if($('.lobibox[data-is-modal=true]').length===0){$('.lobibox-backdrop').remove();$('body').removeClass(Lobibox.base.OPTIONS.bodyClass);}
me._afterClose();}
return this;},setWidth:function(width){var me=this;me.$el.css('width',me._calculateWidth(width));return me;},refreshWidth:function(){this.setWidth(this.$el.width());},refreshHeight:function(){this.setHeight(this.$el.height());},setHeight:function(height){var me=this;me.$el.css('height',me._calculateHeight(height)).find('.lobibox-body').css('height',me._calculateBodyHeight(me.$el.innerHeight()));return me;},setSize:function(width,height){var me=this;me.setWidth(width);me.setHeight(height);return me;},setPosition:function(left,top){var pos;if(typeof left==='number'&&typeof top==='number'){pos={left:left,top:top};}else if(typeof left==='string'){pos=this._calculatePosition(left);}
this.$el.css(pos);return this;},setTitle:function(title){return this.$el.find('.lobibox-title').html(title);},getTitle:function(){return this.$el.find('.lobibox-title').html();},show:function(){var me=this,$body=$('body');me._beforeShow();me.$el.removeClass('lobibox-hidden');$body.append(me.$el);if(me.$options.buttons){var buttons=me.$el.find('.lobibox-footer').children();buttons[0].focus();}
if(me.$options.modal){$body.addClass(Lobibox.base.OPTIONS.bodyClass);me._addBackdrop();}
if(me.$options.delay!==false){setTimeout(function(){me.destroy();},me.$options.delay);}
me._afterShow();return me;}};Lobibox.base={};Lobibox.base.OPTIONS={bodyClass:'lobibox-open',modalClasses:{'error':'lobibox-error','success':'lobibox-success','info':'lobibox-info','warning':'lobibox-warning','confirm':'lobibox-confirm','progress':'lobibox-progress','prompt':'lobibox-prompt','default':'lobibox-default','window':'lobibox-window'},buttonsAlign:['left','center','right'],buttons:{ok:{'class':'lobibox-btn lobibox-btn-default',text:'OK',closeOnClick:true},cancel:{'class':'lobibox-btn lobibox-btn-cancel',text:'Cancel',closeOnClick:true},yes:{'class':'lobibox-btn lobibox-btn-yes',text:'Yes',closeOnClick:true},no:{'class':'lobibox-btn lobibox-btn-no',text:'No',closeOnClick:true}},icons:{bootstrap:{confirm:'glyphicon glyphicon-question-sign',success:'glyphicon glyphicon-ok-sign',error:'glyphicon glyphicon-remove-sign',warning:'glyphicon glyphicon-exclamation-sign',info:'glyphicon glyphicon-info-sign'},fontAwesome:{confirm:'fa fa-question-circle',success:'fa fa-check-circle',error:'fa fa-times-circle',warning:'fa fa-exclamation-circle',info:'fa fa-info-circle'}}};Lobibox.base.DEFAULTS={horizontalOffset:5,verticalOffset:5,width:600,height:'auto',closeButton:true,draggable:false,customBtnClass:'lobibox-btn lobibox-btn-default',modal:true,debug:false,buttonsAlign:'center',closeOnEsc:true,delayToRemove:200,delay:false,baseClass:'animated-super-fast',showClass:'zoomIn',hideClass:'zoomOut',iconSource:'bootstrap',onShow:null,shown:null,beforeClose:null,closed:null};function LobiboxPrompt(type,options){this.$input=null;this.$type='prompt';this.$promptType=type;options=$.extend({},Lobibox.prompt.DEFAULT_OPTIONS,options);this.$options=this._processInput(options);this._init();this.debug(this);}
LobiboxPrompt.prototype=$.extend({},LobiboxBase,{constructor:LobiboxPrompt,_processInput:function(options){var me=this;var mergedOptions=LobiboxBase._processInput.call(me,options);mergedOptions.buttons={ok:Lobibox.base.OPTIONS.buttons.ok,cancel:Lobibox.base.OPTIONS.buttons.cancel};options=$.extend({},mergedOptions,LobiboxPrompt.DEFAULT_OPTIONS,options);return options;},_init:function(){var me=this;LobiboxBase._init.call(me);me.show();},_afterShow:function(){var me=this;me._setContent(me._createInput())._position();me.$input.focus();LobiboxBase._afterShow.call(me);},_createInput:function(){var me=this,label;if(me.$options.multiline){me.$input=$('<textarea></textarea>').attr('rows',me.$options.lines);}else{me.$input=$('<input type="'+me.$promptType+'"/>');}
me.$input.addClass('lobibox-input').attr(me.$options.attrs);if(me.$options.value){me.setValue(me.$options.value);}
if(me.$options.label){label=$('<label>'+me.$options.label+'</label>');}
return $('<div></div>').append(label,me.$input);},setValue:function(val){this.$input.val(val);return this;},getValue:function(){return this.$input.val();},isValid:function(){var me=this,$error=me.$el.find('.lobibox-input-error-message');if(me.$options.required&&!me.getValue()){me.$input.addClass('invalid');if($error.length===0){me.$el.find('.lobibox-body').append('<p class="lobibox-input-error-message">'+me.$options.errorMessage+'</p>');me._position();me.$input.focus();}
return false;}
me.$input.removeClass('invalid');$error.remove();me._position();me.$input.focus();return true;}});LobiboxPrompt.DEFAULT_OPTIONS={width:400,attrs:{},value:'',multiline:false,lines:3,type:'text',label:'',required:true,errorMessage:'The field is required'};function LobiboxConfirm(options){this.$type='confirm';this.$options=this._processInput(options);this._init();this.debug(this);}
LobiboxConfirm.prototype=$.extend({},LobiboxBase,{constructor:LobiboxConfirm,_processInput:function(options){var me=this;var mergedOptions=LobiboxBase._processInput.call(me,options);mergedOptions.buttons={yes:Lobibox.base.OPTIONS.buttons.yes,no:Lobibox.base.OPTIONS.buttons.no};options=$.extend({},mergedOptions,Lobibox.confirm.DEFAULTS,options);return options;},_init:function(){var me=this;LobiboxBase._init.call(me);me.show();},_afterShow:function(){var me=this;var d=$('<div></div>');if(me.$options.iconClass){d.append($('<div class="lobibox-icon-wrapper"></div>').append('<i class="lobibox-icon '+me.$options.iconClass+'"></i>'));}
d.append('<div class="lobibox-body-text-wrapper"><span class="lobibox-body-text">'+me.$options.msg+'</span></div>');me._setContent(d.html());me._position();LobiboxBase._afterShow.call(me);}});Lobibox.confirm.DEFAULTS={title:'Question',width:500};function LobiboxAlert(type,options){this.$type=type;this.$options=this._processInput(options);this._init();this.debug(this);}
LobiboxAlert.prototype=$.extend({},LobiboxBase,{constructor:LobiboxAlert,_processInput:function(options){var me=this;var mergedOptions=LobiboxBase._processInput.call(me,options);mergedOptions.buttons={ok:Lobibox.base.OPTIONS.buttons.ok};options=$.extend({},mergedOptions,Lobibox.alert.OPTIONS[me.$type],Lobibox.alert.DEFAULTS,options);return options;},_init:function(){var me=this;LobiboxBase._init.call(me);me.show();},_afterShow:function(){var me=this;var d=$('<div></div>');if(me.$options.iconClass){d.append($('<div class="lobibox-icon-wrapper"></div>').append('<i class="lobibox-icon '+me.$options.iconClass+'"></i>'));}
d.append('<div class="lobibox-body-text-wrapper"><span class="lobibox-body-text">'+me.$options.msg+'</span></div>');me._setContent(d.html());me._position();LobiboxBase._afterShow.call(me);}});Lobibox.alert.OPTIONS={warning:{title:'Warning'},info:{title:'Information'},success:{title:'Success'},error:{title:'Error'}};Lobibox.alert.DEFAULTS={};function LobiboxProgress(options){this.$type='progress';this.$progressBarElement=null;this.$options=this._processInput(options);this.$progress=0;this._init();this.debug(this);}
LobiboxProgress.prototype=$.extend({},LobiboxBase,{constructor:LobiboxProgress,_processInput:function(options){var me=this;var mergedOptions=LobiboxBase._processInput.call(me,options);options=$.extend({},mergedOptions,Lobibox.progress.DEFAULTS,options);return options;},_init:function(){var me=this;LobiboxBase._init.call(me);me.show();},_afterShow:function(){var me=this;if(me.$options.progressTpl){me.$progressBarElement=$(me.$options.progressTpl);}else{me.$progressBarElement=me._createProgressbar();}
var label;if(me.$options.label){label=$('<label>'+me.$options.label+'</label>');}
var innerHTML=$('<div></div>').append(label,me.$progressBarElement);me._setContent(innerHTML);me._position();LobiboxBase._afterShow.call(me);},_createProgressbar:function(){var me=this;var outer=$('<div class="lobibox-progress-bar-wrapper lobibox-progress-outer"></div>').append('<div class="lobibox-progress-bar lobibox-progress-element"></div>');if(me.$options.showProgressLabel){outer.append('<span class="lobibox-progress-text" data-role="progress-text"></span>');}
return outer;},setProgress:function(progress){var me=this;if(me.$progress===100){return;}
progress=Math.min(100,Math.max(0,progress));me.$progress=progress;me._triggerEvent('progressUpdated');if(me.$progress===100){me._triggerEvent('progressCompleted');}
me.$el.find('.lobibox-progress-element').css('width',progress.toFixed(1)+"%");me.$el.find('[data-role="progress-text"]').html(progress.toFixed(1)+"%");return me;},getProgress:function(){return this.$progress;}});Lobibox.progress.DEFAULTS={width:500,showProgressLabel:true,label:'',progressTpl:false,progressUpdated:null,progressCompleted:null};function LobiboxWindow(type,options){this.$type=type;this.$options=this._processInput(options);this._init();this.debug(this);}
LobiboxWindow.prototype=$.extend({},LobiboxBase,{constructor:LobiboxWindow,_processInput:function(options){var me=this;var mergedOptions=LobiboxBase._processInput.call(me,options);if(options.content&&typeof options.content==='function'){options.content=options.content();}
if(options.content instanceof jQuery){options.content=options.content.clone();}
options=$.extend({},mergedOptions,Lobibox.window.DEFAULTS,options);return options;},_init:function(){var me=this;LobiboxBase._init.call(me);me.setContent(me.$options.content);if(me.$options.url&&me.$options.autoload){if(!me.$options.showAfterLoad){me.show();}
me.load(function(){if(me.$options.showAfterLoad){me.show();}});}else{me.show();}},_afterShow:function(){var me=this;me._position();LobiboxBase._afterShow.call(me);},setParams:function(params){var me=this;me.$options.params=params;return me;},getParams:function(){var me=this;return me.$options.params;},setLoadMethod:function(method){var me=this;me.$options.loadMethod=method;return me;},getLoadMethod:function(){var me=this;return me.$options.loadMethod;},setContent:function(content){var me=this;me.$options.content=content;me.$el.find('.lobibox-body').html('').append(content);return me;},getContent:function(){var me=this;return me.$options.content;},setUrl:function(url){this.$options.url=url;return this;},getUrl:function(){return this.$options.url;},load:function(callback){var me=this;if(!me.$options.url){return me;}
$.ajax(me.$options.url,{method:me.$options.loadMethod,data:me.$options.params}).done(function(res){me.setContent(res);if(callback&&typeof callback==='function'){callback(res);}});return me;}});Lobibox.window.DEFAULTS={width:480,height:600,content:'',url:'',draggable:true,autoload:true,loadMethod:'GET',showAfterLoad:true,params:{}};})();Math.randomString=function(n){var text="";var possible="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";for(var i=0;i<n;i++)
text+=possible.charAt(Math.floor(Math.random()*possible.length));return text;};var Lobibox=Lobibox||{};(function(){var LobiboxNotify=function(type,options){this.$type=null;this.$options=null;this.$el=null;var me=this;var _processInput=function(options){if(options.size==='mini'||options.size==='large'){options=$.extend({},Lobibox.notify.OPTIONS[options.size],options);}
options=$.extend({},Lobibox.notify.OPTIONS[me.$type],Lobibox.notify.DEFAULTS,options);if(options.size!=='mini'&&options.title===true){options.title=Lobibox.notify.OPTIONS[me.$type].title;}else if(options.size==='mini'&&options.title===true){options.title=false;}
if(options.icon===true){options.icon=Lobibox.notify.OPTIONS.icons[options.iconSource][me.$type];}
if(options.sound===true){options.sound=Lobibox.notify.OPTIONS[me.$type].sound;}
if(options.sound){options.sound=options.soundPath+options.sound+options.soundExt;}
return options;};var _appendInWrapper=function($el,$wrapper){if(me.$options.size==='normal'){if($wrapper.hasClass('bottom')){$wrapper.prepend($el);}else{$wrapper.append($el);}}else if(me.$options.size==='mini'){if($wrapper.hasClass('bottom')){$wrapper.prepend($el);}else{$wrapper.append($el);}}else if(me.$options.size==='large'){var tabPane=_createTabPane().append($el);var $li=_createTabControl(tabPane.attr('id'));$wrapper.find('.lb-notify-wrapper').append(tabPane);$wrapper.find('.lb-notify-tabs').append($li);_activateTab($li);$li.find('>a').click(function(){_activateTab($li);});}};var _activateTab=function($li){$li.closest('.lb-notify-tabs').find('>li').removeClass('active');$li.addClass('active');var $current=$($li.find('>a').attr('href'));$current.closest('.lb-notify-wrapper').find('>.lb-tab-pane').removeClass('active');$current.addClass('active')};var _createTabControl=function(tabPaneId){var $li=$('<li></li>',{'class':Lobibox.notify.OPTIONS[me.$type]['class']});$('<a></a>',{'href':'#'+tabPaneId}).append('<i class="tab-control-icon '+me.$options.icon+'"></i>').appendTo($li);return $li;};var _createTabPane=function(){return $('<div></div>',{'class':'lb-tab-pane','id':Math.randomString(10)})};var _createNotifyWrapper=function(){var selector=(me.$options.size==='large'?'.lobibox-notify-wrapper-large':'.lobibox-notify-wrapper')
+"."+me.$options.position.replace(/\s/gi,'.'),$wrapper;$wrapper=$(selector);if($wrapper.length===0){$wrapper=$('<div></div>').addClass(selector.replace(/\./g,' ').trim()).appendTo($('body'));if(me.$options.size==='large'){$wrapper.append($('<ul class="lb-notify-tabs"></ul>')).append($('<div class="lb-notify-wrapper"></div>'));}}
return $wrapper;};var _createNotify=function(){var OPTS=Lobibox.notify.OPTIONS,$iconEl,$innerIconEl,$iconWrapper,$body,$msg,$notify=$('<div></div>',{'class':'lobibox-notify '+OPTS[me.$type]['class']+' '+OPTS['class']+' '+me.$options.showClass});$iconWrapper=$('<div class="lobibox-notify-icon-wrapper"></div>').appendTo($notify);$iconEl=$('<div class="lobibox-notify-icon"></div>').appendTo($iconWrapper);$innerIconEl=$('<div></div>').appendTo($iconEl);if(me.$options.img){$innerIconEl.append('<img src="'+me.$options.img+'"/>');}else if(me.$options.icon){$innerIconEl.append('<div class="icon-el"><i class="'+me.$options.icon+'"></i></div>');}else{$notify.addClass('without-icon');}
$msg=$('<div class="lobibox-notify-msg">'+me.$options.msg+'</div>');if(me.$options.messageHeight!==false){$msg.css('max-height',me.$options.messageHeight);}
$body=$('<div></div>',{'class':'lobibox-notify-body'}).append($msg).appendTo($notify);if(me.$options.title){$body.prepend('<div class="lobibox-notify-title">'+me.$options.title+'<div>');}
_addCloseButton($notify);if(me.$options.size==='normal'||me.$options.size==='mini'){_addCloseOnClick($notify);_addDelay($notify);}
if(me.$options.width){$notify.css('width',_calculateWidth(me.$options.width));}
return $notify;};var _addCloseButton=function($el){if(!me.$options.closable){return;}
$('<span class="lobibox-close">&times;</span>').click(function(ev){ev.preventDefault();ev.stopPropagation();me.remove();}).appendTo($el);};var _addCloseOnClick=function($el){if(!me.$options.closeOnClick){return;}
$el.click(function(){me.remove();});};var _addDelay=function($el){if(!me.$options.delay){return;}
if(me.$options.delayIndicator){var delay=$('<div class="lobibox-delay-indicator"><div></div></div>');$el.append(delay);}
var time=0;var interval=1000/30;var currentTime=new Date().getTime();var timer=setInterval(function(){if(me.$options.continueDelayOnInactiveTab){time=new Date().getTime()-currentTime;}else{time+=interval;}
var width=100*time/me.$options.delay;if(width>=100){width=100;me.remove();timer=clearInterval(timer);}
if(me.$options.delayIndicator){delay.find('div').css('width',width+"%");}},interval);if(me.$options.pauseDelayOnHover){$el.on('mouseenter.lobibox',function(){interval=0;}).on('mouseleave.lobibox',function(){interval=1000/30;});}};var _findTabToActivate=function($li){var $itemToActivate=$li.prev();if($itemToActivate.length===0){$itemToActivate=$li.next();}
if($itemToActivate.length===0){return null;}
return $itemToActivate;};var _calculateWidth=function(width){width=Math.min($(window).outerWidth(),width);return width;};this.remove=function(){me.$el.removeClass(me.$options.showClass).addClass(me.$options.hideClass);var parent=me.$el.parent();var wrapper=parent.closest('.lobibox-notify-wrapper-large');var href='#'+parent.attr('id');var $li=wrapper.find('>.lb-notify-tabs>li:has(a[href="'+href+'"])');$li.addClass(Lobibox.notify.OPTIONS['class']).addClass(me.$options.hideClass);setTimeout(function(){if(me.$options.size==='normal'||me.$options.size==='mini'){me.$el.remove();}else if(me.$options.size==='large'){var $newLi=_findTabToActivate($li);if($newLi){_activateTab($newLi);}
$li.remove();parent.remove();}
var list=Lobibox.notify.list;var ind=list.indexOf(me);list.splice(ind,1);var next=list[ind];if(next&&next.$options.showAfterPrevious){next._init();}},500);return me;};me._init=function(){var $notify=_createNotify();if(me.$options.size==='mini'){$notify.addClass('notify-mini');}
if(typeof me.$options.position==='string'){var $wrapper=_createNotifyWrapper();_appendInWrapper($notify,$wrapper);if($wrapper.hasClass('center')){$wrapper.css('margin-left','-'+($wrapper.width()/2)+"px");}}else{$('body').append($notify);$notify.css({'position':'fixed',left:me.$options.position.left,top:me.$options.position.top})}
me.$el=$notify;if(me.$options.sound){var snd=new Audio(me.$options.sound);snd.play();}
if(me.$options.rounded){me.$el.addClass('rounded');}
me.$el.on('click.lobibox',function(ev){if(me.$options.onClickUrl){window.location.href=me.$options.onClickUrl;}
if(me.$options.onClick&&typeof me.$options.onClick==='function'){me.$options.onClick.call(me,ev);}});me.$el.data('lobibox',me);};this.$type=type;this.$options=_processInput(options);if(!me.$options.showAfterPrevious||Lobibox.notify.list.length===0){this._init();}};Lobibox.notify=function(type,options){if(["default","info","warning","error","success"].indexOf(type)>-1){var lobibox=new LobiboxNotify(type,options);Lobibox.notify.list.push(lobibox);return lobibox;}};Lobibox.notify.list=[];Lobibox.notify.closeAll=function(){var list=Lobibox.notify.list;for(var i in list){list[i].remove();}};Lobibox.notify.DEFAULTS={title:true,size:'normal',soundPath:'sounds/',soundExt:'.ogg',showClass:'fadeInDown',hideClass:'zoomOut',icon:true,msg:'',img:null,closable:true,hideCloseButton:false,delay:5000,delayIndicator:true,closeOnClick:true,width:400,sound:true,position:"bottom right",iconSource:'bootstrap',rounded:false,messageHeight:60,pauseDelayOnHover:true,onClickUrl:null,showAfterPrevious:false,continueDelayOnInactiveTab:true,onClick:null};Lobibox.notify.OPTIONS={'class':'animated-fast',large:{width:500,messageHeight:96},mini:{'class':'notify-mini',messageHeight:32},default:{'class':'lobibox-notify-default','title':'Default',sound:false},success:{'class':'lobibox-notify-success','title':'Success',sound:'sound2'},error:{'class':'lobibox-notify-error','title':'Error',sound:'sound4'},warning:{'class':'lobibox-notify-warning','title':'Warning',sound:'sound5'},info:{'class':'lobibox-notify-info','title':'Information',sound:'sound6'},icons:{bootstrap:{success:'glyphicon glyphicon-ok-sign',error:'glyphicon glyphicon-remove-sign',warning:'glyphicon glyphicon-exclamation-sign',info:'glyphicon glyphicon-info-sign'},fontAwesome:{success:'fa fa-check-circle',error:'fa fa-times-circle',warning:'fa fa-exclamation-circle',info:'fa fa-info-circle'}}};})();