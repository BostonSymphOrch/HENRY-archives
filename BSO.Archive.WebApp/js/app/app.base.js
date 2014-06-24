var BSO = window.BSO || {};

_(BSO).extend(function ($) {
    var View = Backbone.View.extend({
        viewModel: function () {
            return {};
        },

        clearView: function () {
            this.el.innerHTML = "";
        },

        render: function () {
            this.trigger('willRender', this);
            this.el.innerHTML = this.template(this.viewModel());
            this.trigger('didRender', this);
        },

        getBBElement: function (name) {
            return this.el.querySelector('[data-bb="' + name + '"]');
        },

        getBBElementAll: function (name) {
            return this.el.querySelectorAll('[data-bb="' + name + '"]');
        }
    });

    var Model = Backbone.Model.extend({
    });

    var Collection = Backbone.Collection.extend({
    });

    return {
        View: View,
        Model: Model,
        Collection: Collection
    };
}(jQuery));