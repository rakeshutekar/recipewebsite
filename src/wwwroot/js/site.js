// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


// ------------------------------------------------------------------------------------------------------
// CREATE PAGE JAVASCRIPT
// ------------------------------------------------------------------------------------------------------

// Reference for grabbing count of querySelectorAll result
// https://stackoverflow.com/questions/6991494/javascript-getelementbyid-based-on-a-partial-string

// Variables used across all input forms
const textType = "text";
const classType = "form-control";
const inptGrp = "input-group";

// Function prepared to listen to on-click events of the "+" button under the instructions
// form group and add new instruction text areas for users to enter in their information
$("#addBtnInstr").click(function () {
    var instrs = $('[id ^= "instr"]');
    var count = instrs.length;

    // prepare html grouping to insert
    var nextInput = document.createElement("input");
    var nameAttr = "Recipe.Instructions[" + count + "]";
    var idAttr = "instr" + count;
    var placeholderAttr = "Step #" + (count + 1);
    nextInput.setAttribute("id", idAttr);
    nextInput.setAttribute("class", classType);
    nextInput.setAttribute("required", "");
    nextInput.setAttribute("type", textType);
    nextInput.setAttribute("name", nameAttr);
    nextInput.setAttribute("placeholder", placeholderAttr);


    var rmvBtn = $("<button></button>");
    var rmvBtnClass = "btnRmv";
    rmvBtn.attr("class", "btn btn-sm btn-outline-danger").attr("type", "button").addClass(rmvBtnClass);
    rmvBtn.html("Remove");

    // set input group innerHTML value to the button created
    var grpAppend = $("<div></div>");
    grpAppend.attr("class", "input-group-append");
    grpAppend.append(rmvBtn);

    var inputDiv = $("<div></div>");
    var grpClass = "inputGrpInstr";
    inputDiv.addClass(grpClass).addClass(inptGrp);
    inputDiv.append(nextInput);
    inputDiv.append(grpAppend);

    // Insert inside the wrapper
    $("#wrapperInstr").append(inputDiv);

});

// Delete the specific row of using the text area's delete button
$("#wrapperInstr").on("click", ".btnRmv", function (e) {
    e.preventDefault();
    $(this).parent("div").parent("div").remove();

    // Get the count of the number of rows and re-id them
    var instrs = $(".inputGrpInstr");
    var instrCount = instrs.length;
    var counter = 0;

    // Loop over jquery object set and update the proper model names
    instrs.each(function () {
        var name = "Recipe.Instructions[" + counter + "]";
        var placeholder = "Step #" + (counter + 1);
        $(this).children("input").attr("name", name);
        $(this).children("input").attr("placeholder", placeholder);
        counter++;
    });
});


// Function prepared to listen to on-click events of the "+" button under the ingredients
// form group and add new ingredients text areas for users to enter in their information
$("#addBtnIngr").click(function () {
    var instrs = $('[id ^= "ingr"]');
    var count = instrs.length;

    // prepare html grouping to insert
    var nextInput = document.createElement("input");
    var nameAttr = "Recipe.Ingredients[" + count + "]";
    var idAttr = "ingr" + count;
    var placeholderAttr = "Ingredient #" + (count + 1);
    nextInput.setAttribute("id", idAttr);
    nextInput.setAttribute("class", classType);
    nextInput.setAttribute("required", "");
    nextInput.setAttribute("type", textType);
    nextInput.setAttribute("name", nameAttr);
    nextInput.setAttribute("placeholder", placeholderAttr);


    var rmvBtn = $("<button></button>");
    var rmvBtnClass = "btnRmv";
    rmvBtn.attr("class", "btn btn-sm btn-outline-danger").attr("type", "button").addClass(rmvBtnClass);
    rmvBtn.html("Remove");

    // set input group innerHTML value to the button created
    var grpAppend = $("<div></div>");
    grpAppend.attr("class", "input-group-append");
    grpAppend.append(rmvBtn);

    var inputDiv = $("<div></div>");
    var grpClass = "inputGrpIngr";
    inputDiv.addClass(grpClass).addClass(inptGrp);
    inputDiv.append(nextInput);
    inputDiv.append(grpAppend);

    // Insert inside the wrapper
    $("#wrapperIngr").append(inputDiv);

});

// Delete the specific row of using the text area's delete button
$("#wrapperIngr").on("click", ".btnRmv", function (e) {
    e.preventDefault();
    $(this).parent("div").parent("div").remove();

    // Get the count of the number of rows and re-id them
    var instrs = $(".inputGrpIngr");
    var instrCount = instrs.length;
    var counter = 0;

    // Loop over jquery object set and update the proper model names
    instrs.each(function () {
        var name = "Recipe.Ingredients[" + counter + "]";
        var placeholder = "Ingredient #" + (counter + 1);
        $(this).children("input").attr("name", name);
        $(this).children("input").attr("placeholder", placeholder);
        counter++;
    });
});

// Function prepared to listen to on-click events of the "+" button under the Tags
// form group and add new tags text areas for users to enter in their information
$("#addBtnTags").click(function () {
    var tags = $('[id ^= "tag"]');
    var count = tags.length;

    // prepare html grouping to insert
    var nextInput = document.createElement("input");
    var nameAttr = "Recipe.Tags[" + count + "]";
    var idAttr = "tag" + count;
    var placeholderAttr = "Tag #" + (count + 1);
    nextInput.setAttribute("id", idAttr);
    nextInput.setAttribute("class", classType);
    nextInput.setAttribute("required", "");
    nextInput.setAttribute("type", textType);
    nextInput.setAttribute("name", nameAttr);
    nextInput.setAttribute("placeholder", placeholderAttr);


    var rmvBtn = $("<button></button>");
    var rmvBtnClass = "btnRmv";
    rmvBtn.attr("class", "btn btn-sm btn-outline-danger").attr("type", "button").addClass(rmvBtnClass);
    rmvBtn.html("Remove");

    // set input group innerHTML value to the button created
    var grpAppend = $("<div></div>");
    grpAppend.attr("class", "input-group-append");
    grpAppend.append(rmvBtn);

    var inputDiv = $("<div></div>");
    var grpClass = "inputGrpTags";
    inputDiv.addClass(grpClass).addClass(inptGrp);
    inputDiv.append(nextInput);
    inputDiv.append(grpAppend);

    // Insert inside the wrapper
    $("#wrapperTags").append(inputDiv);

});

// Delete the specific row of using the text area's delete button
$("#wrapperTags").on("click", ".btnRmv", function (e) {
    e.preventDefault();
    $(this).parent("div").parent("div").remove();

    // Get the count of the number of rows and re-id them
    var tags = $(".inputGrpTags");
    var tagsCount = tags.length;
    var counter = 0;

    // Loop over jquery object set and update the proper model names as well as
    // placeholder values
    tags.each(function () {
        var name = "Recipe.Tags[" + counter + "]";
        var placeholder = "Tag #" + (counter + 1);
        $(this).children("input").attr("name", name);
        $(this).children("input").attr("placeholder", placeholder);
        counter++;
    });
});


// ------------------------------------------------------------------------------------------------------
// UPDATE PAGE JAVASCRIPT
// ------------------------------------------------------------------------------------------------------