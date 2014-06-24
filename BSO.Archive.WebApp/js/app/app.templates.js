(function() {
  var template = Handlebars.template, templates = Handlebars.templates = Handlebars.templates || {};
templates['artistTable'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  
  return "\r\n<center>\r\n  <span class=\"NoResultsMessage\">No results found</span>\r\n</center>\r\n";
  }

function program3(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n\r\n<table data-bb=\"resultsTable\" class=\"searchResults\">\r\n	<thead>\r\n		<tr>\r\n			<th data-bb=\"sortableTH\" data-sortOptionName=\"SortArtistByName\">Artist/Ensemble &nbsp;<span class=\"triangleSort\"></span></th>\r\n			<th>Instrument/Role<span class=\"triangleSort\"></span></th>\r\n			<th data-bb=\"sortableTH\" data-sortOptionName=\"SortArtistByComposer\">Composer &nbsp;<span class=\"triangleSort\"></span></th>\r\n			<th data-bb=\"sortableTH\" data-sortOptionName=\"SortArtistByWork\">Work &nbsp;<span class=\"triangleSort\"></span></th>\r\n\r\n			<th data-bb=\"sortableTH\" data-sortOptionName=\"SortArtistByConductorCount\"># of times Conductor &nbsp;<span class=\"triangleSort\"></span></th>\r\n			<th data-bb=\"sortableTH\" data-sortOptionName=\"SortArtistBySoloistCount\"># of times Soloist &nbsp;<span class=\"triangleSort\"></span></th>\r\n			<th data-bb=\"sortableTH\" data-sortOptionName=\"SortArtistByEnsembleCount\"># of times Ensemble &nbsp;<span class=\"triangleSort\"></span></th>\r\n		</tr>\r\n	</thead>\r\n	<tbody>\r\n	  ";
  stack1 = helpers.each.call(depth0, depth0.results, {hash:{},inverse:self.noop,fn:self.program(4, program4, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n	</tbody>\r\n</table>\r\n";
  return buffer;
  }
function program4(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n	  <tr class=\"tableColumns\">\r\n			<td class=\"tableColumn\">\r\n				"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ArtistFullName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n			</td>\r\n			<td class=\"tableColumn\">\r\n				<ul>\r\n					";
  stack2 = helpers.each.call(depth0, ((stack1 = depth0.instruments),stack1 == null || stack1 === false ? stack1 : stack1.models), {hash:{},inverse:self.noop,fn:self.program(5, program5, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n				</ul>\r\n			</td>\r\n			<td class=\"tableColumn\">\r\n				"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ComposerFullName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n			</td>\r\n			<td class=\"tableColumn\">\r\n				";
  stack2 = ((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.WorkTitle)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n			</td>\r\n			<td class=\"tableColumn\">\r\n				<a href=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ConductorLink)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n					"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ConductorCount)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n				</a>\r\n					\r\n			</td>\r\n			<td class=\"tableColumn\">\r\n				<a href=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.SoloistLink)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n					"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.SoloistCount)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n				</a>\r\n			</td>\r\n			<td class=\"tableColumn\">\r\n				<a href=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.OrchestraLink)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n					"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.EnsembleCount)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n				</a>\r\n			</td>\r\n		</tr>\r\n		";
  return buffer;
  }
function program5(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n					<li>\r\n						"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.Instrument)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n					</li>\r\n					";
  return buffer;
  }

  buffer += "﻿";
  stack1 = helpers.unless.call(depth0, depth0.hasResults, {hash:{},inverse:self.program(3, program3, data),fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  return buffer;
  });
templates['filterArea'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, stack2, functionType="function", escapeExpression=this.escapeExpression, self=this, helperMissing=helpers.helperMissing;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n  ";
  stack1 = helpers['if'].call(depth0, depth0.overMax, {hash:{},inverse:self.program(4, program4, data),fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <div class=\"NoResultsMessage\">\r\n      <p>\r\n        <span>Too Many Results</span> You are viewing the first ";
  if (stack1 = helpers.maxResults) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.maxResults; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + " results out of ";
  if (stack1 = helpers.resultCount) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.resultCount; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + ". To view a full result set, please narrow your search by adding criteria or applying filters.\r\n      </p>\r\n    </div>\r\n  ";
  return buffer;
  }

function program4(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <div>\r\n      <p class=\"resultCount\">Search results: ";
  if (stack1 = helpers.resultCount) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.resultCount; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "<p>\r\n    </div>\r\n  ";
  return buffer;
  }

function program6(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n<div id=\"filterBackground\" ";
  stack1 = helpers.unless.call(depth0, depth0.showFilterArea, {hash:{},inverse:self.noop,fn:self.program(7, program7, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += ">\r\n  <div id=\"filterResults\">\r\n    <h4>Available Filters</h4>\r\n    ";
  stack2 = helpers.each.call(depth0, ((stack1 = depth0.availableFilters),stack1 == null || stack1 === false ? stack1 : stack1.models), {hash:{},inverse:self.noop,fn:self.program(9, program9, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n    ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.selectedFilters),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.noop,fn:self.program(21, program21, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n\r\n\r\n</div>\r\n</div>\r\n";
  return buffer;
  }
function program7(depth0,data) {
  
  
  return "style=\"display:none\"";
  }

function program9(depth0,data) {
  
  var buffer = "", stack1, stack2, options;
  buffer += "\r\n    <div data-bb=\"filterOptionDropdown\">\r\n      ";
  options = {hash:{},inverse:self.program(13, program13, data),fn:self.program(10, program10, data),data:data};
  stack2 = ((stack1 = helpers.ifShouldCreateFilterGroup || depth0.ifShouldCreateFilterGroup),stack1 ? stack1.call(depth0, depth0.filters, options) : helperMissing.call(depth0, "ifShouldCreateFilterGroup", depth0.filters, options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n\r\n      ";
  options = {hash:{},inverse:self.noop,fn:self.program(15, program15, data),data:data};
  stack2 = ((stack1 = helpers.ifShouldCreateFilterGroup || depth0.ifShouldCreateFilterGroup),stack1 ? stack1.call(depth0, depth0.filters, options) : helperMissing.call(depth0, "ifShouldCreateFilterGroup", depth0.filters, options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n    </div>\r\n    ";
  return buffer;
  }
function program10(depth0,data) {
  
  var buffer = "", stack1, stack2, options;
  buffer += "\r\n      <span data-bb=\"filterToggle\" ";
  options = {hash:{},inverse:self.noop,fn:self.program(11, program11, data),data:data};
  stack2 = ((stack1 = helpers.hasActiveFilters || depth0.hasActiveFilters),stack1 ? stack1.call(depth0, depth0.filters, options) : helperMissing.call(depth0, "hasActiveFilters", depth0.filters, options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += ">\r\n        <strong>"
    + escapeExpression(((stack1 = depth0.Category),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</strong>\r\n      </span>\r\n      ";
  return buffer;
  }
function program11(depth0,data) {
  
  
  return "class=\"filterSelected\"";
  }

function program13(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n      <span data-bb=\"emptyFilter\">\r\n        <strong>"
    + escapeExpression(((stack1 = depth0.Category),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</strong>\r\n      </span>\r\n      ";
  return buffer;
  }

function program15(depth0,data) {
  
  var buffer = "", stack1, stack2, options;
  buffer += "\r\n      <span data-bb=\"filterOptionsList\">\r\n        <ul>\r\n          ";
  options = {hash:{},inverse:self.noop,fn:self.program(16, program16, data),data:data};
  stack2 = ((stack1 = helpers.ifNotEqual || depth0.ifNotEqual),stack1 ? stack1.call(depth0, ((stack1 = ((stack1 = depth0.filters),stack1 == null || stack1 === false ? stack1 : stack1.models)),stack1 == null || stack1 === false ? stack1 : stack1.length), 1, options) : helperMissing.call(depth0, "ifNotEqual", ((stack1 = ((stack1 = depth0.filters),stack1 == null || stack1 === false ? stack1 : stack1.models)),stack1 == null || stack1 === false ? stack1 : stack1.length), 1, options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n\r\n          ";
  stack2 = helpers.each.call(depth0, ((stack1 = depth0.filters),stack1 == null || stack1 === false ? stack1 : stack1.models), {hash:{},inverse:self.noop,fn:self.program(18, program18, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n        </ul>\r\n      </span>\r\n      ";
  return buffer;
  }
function program16(depth0,data) {
  
  
  return "\r\n          <li data-bb=\"filterOption\">\r\n            <label>\r\n              <input type=\"checkbox\" data-bb=\"selectAllFilterOptions\" />Select All\r\n            </label>\r\n          </li>\r\n          ";
  }

function program18(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n          <li data-bb=\"filterOption\" data-category=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.category)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-key=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.key)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n            <label>\r\n              <input type=\"checkbox\" data-bb=\"filterOptionItemCheckbox\" data-category=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.category)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-key=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.key)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\"\r\n              ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.active), {hash:{},inverse:self.noop,fn:self.program(19, program19, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += " />\r\n              ";
  stack2 = ((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n            </label>\r\n          </li>\r\n          ";
  return buffer;
  }
function program19(depth0,data) {
  
  
  return "checked";
  }

function program21(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <br/><br/>\r\n    <h4>Selected Filters</h4>\r\n    <div id=\"selectedFilters\">\r\n      ";
  stack1 = helpers.each.call(depth0, depth0.selectedFilters, {hash:{},inverse:self.noop,fn:self.program(22, program22, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n\r\n\r\n  </div>\r\n  ";
  return buffer;
  }
function program22(depth0,data) {
  
  var buffer = "", stack1, stack2, options;
  buffer += "\r\n      ";
  options = {hash:{},inverse:self.noop,fn:self.program(23, program23, data),data:data};
  stack2 = ((stack1 = helpers.hasActiveFilters || depth0.hasActiveFilters),stack1 ? stack1.call(depth0, depth0, options) : helperMissing.call(depth0, "hasActiveFilters", depth0, options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n    ";
  return buffer;
  }
function program23(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n      <div data-bb=\"selectedFilterGroup\">\r\n       <h5>"
    + escapeExpression(((stack1 = depth0.Category),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</h5>\r\n       <ul>\r\n        ";
  stack2 = helpers.each.call(depth0, ((stack1 = depth0.filters),stack1 == null || stack1 === false ? stack1 : stack1.models), {hash:{},inverse:self.noop,fn:self.program(24, program24, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n      </ul>\r\n    </div>\r\n\r\n    ";
  return buffer;
  }
function program24(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n        ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.active), {hash:{},inverse:self.noop,fn:self.program(25, program25, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n        ";
  return buffer;
  }
function program25(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n        <li data-bb=\"selectedFilterValue\" data-category=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.category)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-key=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.key)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n          ";
  stack2 = ((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += " &nbsp;\r\n          <span data-bb=\"removeFilterButton\" class=\"remove\" data-category=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.category)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-key=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.key)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n            <i class='icon-minus-sign'></i>\r\n          </span>\r\n        </li>\r\n        ";
  return buffer;
  }

  buffer += "﻿";
  stack1 = helpers['if'].call(depth0, depth0.resultCount, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n\r\n<a href=\"#\" id=\"filterAreaShowHideButton\" class=\"filterAreaShowHideButton\">Filter Results</a>\r\n<a href=\"#\" class=\"exportResults\">Export Results (XLS)</a>\r\n<a href='#' class=\"shareSingleResult\">Share Search</a>\r\n<!--<a href=\"#\" class=\"shareSearch\">Share Search</a>-->\r\n\r\n";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.availableFilters),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.noop,fn:self.program(6, program6, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n";
  return buffer;
  });
templates['filterOptionDropdown'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, stack2, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n  <h3 class=\"selectedFilters\">Your Selected Filters</h3>\r\n  <a href=\"#\" class=\"exportResults\">Export Results (CSV)</a>\r\n\r\n  <div data-bb=\"selectedDataFilterSetView\" class=\"filterResultsContainer\">\r\n  </div>\r\n\r\n  <div id=\"filterBackground clearfix\">\r\n    <div id=\"filterResults\">\r\n      <h4>Select Filters</h4>\r\n\r\n      <ul data-bb=\"filterResults\">\r\n        ";
  stack2 = helpers.each.call(depth0, ((stack1 = depth0.filters),stack1 == null || stack1 === false ? stack1 : stack1.models), {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n      </ul>\r\n\r\n\r\n    </div>\r\n  </div>\r\n";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n          <div data-bb=\"filterOptionDropdown\">\r\n            ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.models),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.program(5, program5, data),fn:self.program(3, program3, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n\r\n            <span>\r\n              ";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.models),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.noop,fn:self.program(7, program7, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n            </span>\r\n          </div>\r\n        ";
  return buffer;
  }
function program3(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n              <span data-bb=\"filterToggle\">\r\n                <strong>"
    + escapeExpression(((stack1 = depth0.Category),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</strong>\r\n              </span>\r\n            ";
  return buffer;
  }

function program5(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n              <span class=\"emptyFilter\">\r\n                <strong>"
    + escapeExpression(((stack1 = depth0.Category),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</strong>\r\n              </span>    \r\n            ";
  return buffer;
  }

function program7(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n                <ul data-bb=\"filterOptionsList\">\r\n                  <li>\r\n                    <label>\r\n                    <input type=\"checkbox\" data-value=\"All\" name=\"cbSelectAll\" data-bb=\"selectAll\" />Select All</label>\r\n                  </li>\r\n\r\n                  ";
  stack1 = helpers.each.call(depth0, depth0.models, {hash:{},inverse:self.noop,fn:self.program(8, program8, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n                </ul>\r\n              ";
  return buffer;
  }
function program8(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n                  <li data-bb=\"filterOption\">\r\n                    <label>\r\n                      <input type=\"checkbox\" data-bb=\"filterOptionItemCheckbox\" data-category=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.category)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-key=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.key)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\"/>\r\n                      "
    + escapeExpression((typeof depth0 === functionType ? depth0.apply(depth0) : depth0))
    + "\r\n                    </label>\r\n                  </li>\r\n                ";
  return buffer;
  }

  buffer += "﻿";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.filters),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n";
  return buffer;
  });
templates['filterSelect'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n    <ul data-bb=\"filterOptionsList\">\r\n        <li>\r\n          <label>\r\n            <input type=\"checkbox\" data-value=\"All\" name=\"cbSelectAll\" data-bb=\"selectAll\" />\r\n            Select All</label>\r\n        </li>\r\n      ";
  stack1 = helpers.each.call(depth0, depth0.options, {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </ul>   \r\n    ";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "";
  buffer += "\r\n      <li data-bb=\"filterOption\">\r\n        <label>\r\n          <input type=\"checkbox\" data-value=\""
    + escapeExpression((typeof depth0 === functionType ? depth0.apply(depth0) : depth0))
    + "\"/>\r\n          "
    + escapeExpression((typeof depth0 === functionType ? depth0.apply(depth0) : depth0))
    + "\r\n        </label>\r\n      </li>\r\n      ";
  return buffer;
  }

  buffer += "﻿<div data-bb=\"filterOptionDropdown\">\r\n  <span data-bb=\"filterToggle\">\r\n    <strong>";
  if (stack1 = helpers.name) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.name; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</strong>\r\n  </span>\r\n\r\n  <span>\r\n    ";
  stack1 = helpers['if'].call(depth0, depth0.options, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n  </span>\r\n</div>";
  return buffer;
  });
templates['performanceTable'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var stack1, functionType="function", escapeExpression=this.escapeExpression, self=this, helperMissing=helpers.helperMissing;

function program1(depth0,data) {
  
  
  return "\r\n<center>\r\n  <div class=\"NoResultsMessage\">No results found</div>\r\n</center>\r\n";
  }

function program3(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n\r\n<table data-bb=\"resultsTable\" class=\"searchResults\">\r\n  <thead>\r\n	<tr>\r\n	  <th data-bb=\"sortableTH\" data-sortOptionName=\"SortPerformanceByDate\">\r\n		Date &nbsp;<span class=\"triangleSort\"></span>\r\n	  </th>\r\n	  <th data-bb=\"sortableTH\" data-sortOptionName=\"SortPerformanceBySeason\">\r\n		Season &nbsp;<span class=\"triangleSort\"></span>\r\n	  </th>\r\n	  <th data-bb=\"sortableTH\" data-sortOptionName=\"WorkListSort\">\r\n		Composer/<br />\r\n		Work &nbsp;<span class=\"triangleSort\">\r\n		</span>\r\n	  </th>\r\n	  <th data-bb=\"sortableTH\" data-sortOptionName=\"ArtistListSort\">\r\n		Soloist/<br />\r\n		Instrument &nbsp;<span class=\"triangleSort\"></span>\r\n	  </th>\r\n	  <th data-bb=\"sortableTH\" data-sortOptionName=\"SortPerformanceByConductor\">\r\n		Conductor &nbsp;<span class=\"triangleSort\"></span>\r\n	  </th>\r\n	  <th data-bb=\"sortableTH\" data-sortOptionName=\"SortPerformanceByOrchestra\">\r\n		Orchestra &nbsp;<span class=\"triangleSort\"></span>\r\n	  </th>\r\n	  <th data-bb=\"sortableTH\" data-sortOptionName=\"SortPerformanceByVenue\">\r\n		Venue &nbsp;<span class=\"triangleSort\"></span>\r\n	  </th>\r\n	  <th data-bb=\"programBook\">Program Book</th>\r\n	</tr>\r\n  </thead>\r\n\r\n  <tbody>\r\n	";
  stack1 = helpers.each.call(depth0, depth0.results, {hash:{},inverse:self.noop,fn:self.programWithDepth(4, program4, data, depth0),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n  </tbody>\r\n</table>\r\n";
  return buffer;
  }
function program4(depth0,data,depth1) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n	<tr class=\"tableColumns\">\r\n	  <td class=\"tableColumn\"> "
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.EventDate)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + " </td>\r\n	  <td class=\"tableColumn\"> "
    + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.season),stack1 == null || stack1 === false ? stack1 : stack1.attributes)),stack1 == null || stack1 === false ? stack1 : stack1.SeasonName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + " </td>\r\n	  <td colspan=\"2\" class=\"tableColumn\">\r\n		";
  stack2 = helpers.each.call(depth0, depth0.visibleWorks, {hash:{},inverse:self.noop,fn:self.programWithDepth(5, program5, data, depth0, depth1),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n	  </td>\r\n\r\n	  <td>\r\n		"
    + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.conductor),stack1 == null || stack1 === false ? stack1 : stack1.attributes)),stack1 == null || stack1 === false ? stack1 : stack1.ConductorFullName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n	  </td>\r\n\r\n	  <td>\r\n		"
    + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.orchestra),stack1 == null || stack1 === false ? stack1 : stack1.attributes)),stack1 == null || stack1 === false ? stack1 : stack1.OrchestraName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n	  </td>\r\n\r\n	  <td>\r\n		"
    + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.venue),stack1 == null || stack1 === false ? stack1 : stack1.attributes)),stack1 == null || stack1 === false ? stack1 : stack1.VenueName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + " </br> "
    + escapeExpression(((stack1 = ((stack1 = ((stack1 = depth0.venue),stack1 == null || stack1 === false ? stack1 : stack1.attributes)),stack1 == null || stack1 === false ? stack1 : stack1.VenueLocation)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n	  </td>\r\n	  <td data-bb=\"programBook\">\r\n		<a class=\"tableLinks\" href=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.DetailLink)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" target=\"_blank\">\r\n		  <i class=\"icon-search\"></i>\r\n		</a>\r\n\r\n		";
  stack2 = helpers['if'].call(depth0, ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.EventProgramNo), {hash:{},inverse:self.program(16, program16, data),fn:self.program(14, program14, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n	  </td>\r\n	</tr>\r\n	";
  return buffer;
  }
function program5(depth0,data,depth1,depth2) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n		<div>\r\n		  <ul class=\"columnInnerLeft\">\r\n			<li>\r\n			  "
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ComposerFullName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + " / <span>";
  stack2 = ((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.WorkTitle)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "</span>\r\n			</li>\r\n			";
  stack2 = helpers['if'].call(depth0, ((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.Arranger)),stack1 == null || stack1 === false ? stack1 : stack1.length), {hash:{},inverse:self.noop,fn:self.program(6, program6, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n		  </ul>\r\n		  <ul class=\"columnInnerRight\">\r\n			";
  stack2 = helpers['if'].call(depth0, depth2.exportXLS, {hash:{},inverse:self.programWithDepth(11, program11, data, depth1),fn:self.program(8, program8, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n		</ul>\r\n	</div>\r\n	";
  return buffer;
  }
function program6(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n			<li>\r\n			  Arr: "
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.Arranger)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n			</li>\r\n			";
  return buffer;
  }

function program8(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n				";
  stack2 = helpers.each.call(depth0, ((stack1 = depth0.artists),stack1 == null || stack1 === false ? stack1 : stack1.models), {hash:{},inverse:self.noop,fn:self.program(9, program9, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n			";
  return buffer;
  }
function program9(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n				<li>\r\n					"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ArtistFullName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + " / <span>"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ArtistInstrument)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</span>\r\n				</li>\r\n				";
  return buffer;
  }

function program11(depth0,data,depth2) {
  
  var buffer = "", stack1, stack2, options;
  buffer += "\r\n				";
  options = {hash:{},inverse:self.noop,fn:self.program(9, program9, data),data:data};
  stack2 = ((stack1 = helpers.eachWithLimit || depth0.eachWithLimit),stack1 ? stack1.call(depth0, ((stack1 = depth0.artists),stack1 == null || stack1 === false ? stack1 : stack1.models), 2, options) : helperMissing.call(depth0, "eachWithLimit", ((stack1 = depth0.artists),stack1 == null || stack1 === false ? stack1 : stack1.models), 2, options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n				";
  options = {hash:{},inverse:self.noop,fn:self.programWithDepth(12, program12, data, depth2),data:data};
  stack2 = ((stack1 = helpers.greaterThan || depth0.greaterThan),stack1 ? stack1.call(depth0, ((stack1 = ((stack1 = depth0.artists),stack1 == null || stack1 === false ? stack1 : stack1.models)),stack1 == null || stack1 === false ? stack1 : stack1.length), 2, options) : helperMissing.call(depth0, "greaterThan", ((stack1 = ((stack1 = depth0.artists),stack1 == null || stack1 === false ? stack1 : stack1.models)),stack1 == null || stack1 === false ? stack1 : stack1.length), 2, options));
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n			";
  return buffer;
  }
function program12(depth0,data,depth3) {
  
  var buffer = "", stack1;
  buffer += "\r\n				<li>\r\n					<a class=\"moreArtistsLink\" href=\"Detail.aspx?UniqueKey="
    + escapeExpression(((stack1 = ((stack1 = depth3.attributes),stack1 == null || stack1 === false ? stack1 : stack1.EventId)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" target=\"_blank\">more...</a>\r\n				</li>\r\n				";
  return buffer;
  }

function program14(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n		<a class=\"tableLinks\" href=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ProgramBookLink)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" target=\"_blank\">\r\n		  <img class=\"searchPDF\" src=\"/images/pdf.png\" alt=\"pdf icon\" />\r\n		</a>\r\n		";
  return buffer;
  }

function program16(depth0,data) {
  
  
  return "\r\n		<a href=\"#\" class=\"tableLinks\">\r\n		<img class=\"searchPDF disabledPDF\" src=\"/images/pdf_grey.png\" alt=\"pdf icon\" />\r\n		</a>\r\n		";
  }

  stack1 = helpers.unless.call(depth0, depth0.hasResults, {hash:{},inverse:self.program(3, program3, data),fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { return stack1; }
  else { return ''; }
  });
templates['repertoireTable'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  
  return "\r\n<center>\r\n  <span class=\"NoResultsMessage\">No results found</span>\r\n</center>\r\n";
  }

function program3(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n\r\n<table data-bb=\"resultsTable\" class=\"searchResults\">\r\n    <thead>\r\n        <tr>\r\n            <th data-bb=\"sortableTH\" data-sortOptionName=\"SortRepertoireByComposer\">Composer &nbsp;<span class=\"triangleSort\"></span></th>\r\n            <th data-bb=\"sortableTH\" data-sortOptionName=\"SortRepertoireByWork\">Work &nbsp;<span class=\"triangleSort\"></span></th>\r\n            <th data-bb=\"sortableTH\" data-sortOptionName=\"SortRepertoireByArranger\">Arranger &nbsp;<span class=\"triangleSort\"></span></th>\r\n            <th data-bb=\"sortableTH\" data-sortOptionName=\"SortRepertoireByPerformanceCount\"># of times performed &nbsp;<span class=\"triangleSort\"></span></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n      ";
  stack1 = helpers.each.call(depth0, depth0.results, {hash:{},inverse:self.noop,fn:self.program(4, program4, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\r\n    </tbody>\r\n</table>\r\n";
  return buffer;
  }
function program4(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n      <tr>\r\n            <td>\r\n                "
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.ComposerFullName)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n            </td>\r\n            <td>\r\n                ";
  stack2 = ((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.WorkTitle)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1);
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n            </td>\r\n            <td>\r\n                "
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.Arranger)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n            </td>\r\n            <td>\r\n                <a href=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.WorkLink)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n                "
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.PerformanceCount)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\r\n                </a>\r\n            </td>\r\n        </tr>\r\n        ";
  return buffer;
  }

  buffer += "﻿";
  stack1 = helpers.unless.call(depth0, depth0.hasResults, {hash:{},inverse:self.program(3, program3, data),fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  return buffer;
  });
templates['resultsTable'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  


  return "﻿";
  });
templates['selectedDataFilter'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression;


  buffer += "﻿";
  if (stack1 = helpers.value) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.value; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\r\n";
  if (stack1 = helpers.key) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.key; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1);
  return buffer;
  });
templates['selectedFilters'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1, stack2;
  buffer += "\r\n	<div data-bb=\"selectedFilterList\" data-bb=\""
    + escapeExpression(((stack1 = ((stack1 = data),stack1 == null || stack1 === false ? stack1 : stack1.key)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\">\r\n	</br>\r\n		<h4>"
    + escapeExpression(((stack1 = ((stack1 = data),stack1 == null || stack1 === false ? stack1 : stack1.key)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "</h4>\r\n		<ul>\r\n		";
  stack2 = helpers.each.call(depth0, depth0, {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack2 || stack2 === 0) { buffer += stack2; }
  buffer += "\r\n		</ul>\r\n		</br>\r\n	</div>\r\n";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\r\n			<li>\r\n			 	"
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + " &nbsp;<span data-bb=\"removeSelectedFilter\" class=\"remove\" data-value=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.value)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\" data-filtername=\""
    + escapeExpression(((stack1 = ((stack1 = depth0.attributes),stack1 == null || stack1 === false ? stack1 : stack1.name)),typeof stack1 === functionType ? stack1.apply(depth0) : stack1))
    + "\"><i class='icon-minus-sign'></i></span>\r\n			</li>\r\n		";
  return buffer;
  }

  buffer += "﻿";
  stack1 = helpers.each.call(depth0, depth0.filterGroups, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  return buffer;
  });
templates['tableRow'] = template(function (Handlebars,depth0,helpers,partials,data) {
  this.compilerInfo = [4,'>= 1.0.0'];
helpers = this.merge(helpers, Handlebars.helpers); data = data || {};
  var buffer = "", stack1, functionType="function", escapeExpression=this.escapeExpression, self=this;

function program1(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\n  <ul>\n    ";
  stack1 = helpers.each.call(depth0, depth0.innerResults, {hash:{},inverse:self.noop,fn:self.program(2, program2, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\n  </ul>\n  ";
  return buffer;
  }
function program2(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\n    <li>";
  if (stack1 = helpers.ComposerFullName) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.ComposerFullName; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + " / ";
  if (stack1 = helpers.WorkTitle) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.WorkTitle; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + " <br/> ";
  if (stack1 = helpers.WorkArrangement) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.WorkArrangement; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + " <br /> ";
  if (stack1 = helpers.WorkPremiere) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.WorkPremiere; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\n    </li>\n    ";
  return buffer;
  }

function program4(depth0,data) {
  
  var buffer = "", stack1;
  buffer += "\n            <li>\n                ";
  if (stack1 = helpers.ArtistFullName) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.ArtistFullName; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + " <br/> ";
  if (stack1 = helpers.Instrument1) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.Instrument1; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\n            </li>\n        ";
  return buffer;
  }

  buffer += "<td>\n    ";
  if (stack1 = helpers.EventFullDate) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.EventFullDate; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + " <br/> \n</td>\n<td>";
  if (stack1 = helpers.SeasonName) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.SeasonName; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</td>\n<td>\n  ";
  stack1 = helpers['if'].call(depth0, depth0.innerResults, {hash:{},inverse:self.noop,fn:self.program(1, program1, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\n</td>\n<td>\n    <ul>\n        ";
  stack1 = helpers.each.call(depth0, depth0.innerResults, {hash:{},inverse:self.noop,fn:self.program(4, program4, data),data:data});
  if(stack1 || stack1 === 0) { buffer += stack1; }
  buffer += "\n    </ul>\n</td>\n<td>";
  if (stack1 = helpers.ConductorFullName) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.ConductorFullName; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</td>\n<td>";
  if (stack1 = helpers.OrchestraName) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.OrchestraName; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</td>\n<td>";
  if (stack1 = helpers.VenueName) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.VenueName; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "</td>\n<td>\n  <a href=\"Detail.aspx?id=";
  if (stack1 = helpers.EventID) { stack1 = stack1.call(depth0, {hash:{},data:data}); }
  else { stack1 = depth0.EventID; stack1 = typeof stack1 === functionType ? stack1.apply(depth0) : stack1; }
  buffer += escapeExpression(stack1)
    + "\" target=\"_blank\"><i class=\"icon-search\"></i>\n  </a>\n</td>\n<td>\n  <a href=\"#\">\n    <img class=\"searchPDF\" src=\"/images/pdf.png\" alt=\"pdf icon\" />\n  </a>\n  <p class=\"searchP\">VIEW <br />PDF</p>\n</td>";
  return buffer;
  });
})();