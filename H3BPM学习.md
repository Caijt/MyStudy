```javascript
$.MvcSheetUI.GetControlValue("fieldName",rownum)
//根据字段编码获取值，如果是明细表，那传入rownum，会得到明细第rownum行的对象，否则返回一个数组

$.MvcSheetUI.SetControlValue("fieldName",value,rownum)
//根据字段编码设置值，如果是明细表，那传入rownum，会得设置明细第rownum行的对象

//表单验证事件
$.MvcSheet.Validate = function () {}

//同步两个明细表，把Details的明细表数据同步到Details2的明细表中
var data = $.MvcSheetUI.GetControlValue("Details");
var m = $("#Details2").SheetUIManager();
m._Clear();
for (var i = 0; i < data.length; i++) {
    m._AddRow(); //添加行
    var row = i + 1;
    $.MvcSheetUI.SetControlValue("Details2.Field1", data[i].Field1, row);
    $.MvcSheetUI.SetControlValue("Details2.Field2", data[i].Field2, row);
    $.MvcSheetUI.SetControlValue("Details2.Field3", data[i].Field3, row);
}

//获取控件管理对象
$("#ControlId").SheetUIManager();

//SheetUIManager对象结构
{
    AlternatingRowStyle: ""
    ButtonId: "ClosingDetails_891"
    Css: {inputMouseOut: "inputMouseOut", inputMouseMove: "inputMouseMove", inputMouseEnter: "", inputError: "inputError", InvalidText: "InvalidText"}
    DataField: "ClosingDetails"
    DataItem: {L: 41, O: "VEM", V: {…}, N: "结案明细表", RowNum: 0}
    DefaultRow: {DataItems: {…}}
    DefaultRowCount: 0
    DisplayAdd: false
    DisplayClear: ""
    DisplayDelete: false
    DisplayExport: ""
    DisplayImport: ""
    DisplayInsertRow: ""
    DisplaySequenceNo: true
    DisplaySummary: true
    DisplayText: "批量录入数据"
    Editable: true
    Element: table#Control19.SheetGridView
    FormatAction: "GetFormatValue"
    InputMappings: ""
    IsHtml5: true
    L: 41
    LogicType: 41
    MobileContainer: null
    N: "结案明细表"
    O: "VEM"
    OnAdded: ""
    OnEditorSaving: ""
    OnPreAdd: ""
    OnPreRemove: ""
    OnRemoved: ""
    OnRendered: ""
    Options: {DataField: "ClosingDetails", DisplayAdd: false, DisplayClear: "", DisplayDelete: false, DisplayExport: "", …}
    Originate: true
    OutputMappings: ""
    PopupHeight: "400px"
    PopupWidth: "600px"
    PopupWindow: "None"
    PortalRoot: "/Portal"
    QueryCode: ""
    Required: false
    RowCount: 1
    RowNum: 2
    SchemaCode: ""
    SheetInfo: {StartActivityCode: "Activity2", ActivityCode: "Activity2", DisplayName: "出差申请结案单", SchemaCode: "BusinessTripApply", UserID: "18f923a7-5a5e-426d-94ae-a55ad1a4b239", …}
    Summary: init [tr.footer, prevObject: init(1), context: table#Control19.SheetGridView, selector: "tr.footer"]
    TrackVisiable: false
    Type: "SheetGridView"
    V: {R: Array(1), T: {…}}
    ValidateResult: true
    ViewInNewContainer: ""
    VirtualPageNum: 10
    VirtualPaging: ""
    Visiable: true
    addbtn: init [div]
    batchInputbtn: init [div]
    clearbtn: init [div]
    dataContainerDivHeight: 0
    exportbtn: init [div]
    importbtn: init [div]
    loadNum: 10
    newRow: init [tr.rows, prevObject: init(1), context: table#Control19.SheetGridView]
    originateValue: null
    pageIndex: 0
    template: init [tr.template, prevObject: init(1), context: table#Control19.SheetGridView, selector: "tr.template"]
    __proto__: $.MvcSheetUI.IControl
}
```





### 链接

```html
<a class="SheetHyperLink" href="<%=this.ActionContext.BizObject["cn_plm_url"] %>" target="_blank"><%=this.ActionContext.BizObject["cn_plm_number"] %></a>
    
<div class="nav-icon fa  fa-chevron-right bannerTitle"><label>页签标题</label></div>
```



参与者也可以设置vaildationrule，虽然在拖拽界面上没有看到属性，但可以在代码里手工加上data-vaildationrule=""，也可以实现效果





#### MvcDataItem的O属性意义

V 可见

E 可写

R 必填

M 移动



关联关系对应的属性类型为BizObject，可以用GetAssociation获取得到

