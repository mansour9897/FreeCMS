/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    //config.language = 'fa';
    config.contentsLangDirection='rtl';
    config.filebrowserBrowseUrl = '/Folder/CkEditorSelectImage';
    config.toolbar = "Custome";
    config.toolbar_Custome =

        [
            ["Source", "-", "NewPage", "Preview", "-"],
            ["Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "SpellChecker", "Scayt"],
            ["Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat"],

            ["Button", "ImageButton", "HiddenField"],
            "/",
            ["Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript"],
            ["NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote"],

            ["JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock"],
            ["BidiLtr", "BidiRtl", "Video"],
            ["Link", "Unlink", "Anchor"],
            "/",
            ["Image", "Table", "HorizontalRule", "Smiley", "SpecialChar"],
            ["Styles", "Format", "Font", "FontSize"],
            ["TextColor", "BGColor"],
        ];
};
