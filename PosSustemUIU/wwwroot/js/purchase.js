
var purchsePage = new Vue({
    el: '#purchsePage',
    data: {
        productError: false,
        priceError: false,
        quantityError: false,
        expireDateError: false,
        product: null,
        productName: null,
        price: null,
        quantity: null,
        expireDate: null,
        products: [],
        selectedProducts: [],
        returnId: null,
        internalMemo: null,
        externalMemo: null,
        paidAmount: null,
        purchaseNote: null,
        purchaseNote: null,
        deliveryNote: null,
        otherNote: null,
        totalPrice:0,
        totalQuantity: 0
    },
    methods: {
        addProductToList() {
            let self = this;
            if (self.isValid()) {
                //add product to list
                self.selectedProducts.push(
                    { 
                        productId: self.product,
                        productName: self.productName,
                        price: self.price,
                        quantity: self.quantity,
                        expireDate:self.expireDate
                    }
                );
                //update total price and quantity
                self.totalPrice =0;
                self.totalQuantity =0;

                self.selectedProducts.map(function (product) {
                    console.log(parseInt(product.price) * parseInt(product.quantity));
                    self.totalPrice += parseInt(product.price) * parseInt(product.quantity);
                    self.totalQuantity += parseInt(product.quantity);
                });
                self.clearField();
            }
        },
        isValid() {
            let self = this;
            let isvalid = false;
            self.clearError();
            self.product = $("#product").select2('val');
            self.productName = $("#product").select2('data')[0].text;
            self.expireDate = $("#expireDate").val();
            if (self.product == null) { self.productError = true; isvalid = false; }
            else if (self.price == null) { self.priceError = true; isvalid = false; }
            else if (self.quantity == null) { self.quantityError = true; isvalid = false; }
            else if (self.expireDate == null || self.expireDate == '') { self.expireDateError = true; isvalid = false; }
            else isvalid = true;
            return isvalid;
        },
        clearError() {
            let self = this;
            self.productError = false;
            self.priceError = false;
            self.quantityError = false;
            self.expireDateError = false;
        },
        clearField() {
            let self = this;
            self.product = '';
            self.price = '';
            self.quantity = '';
            self.expireDate = '';
            $('#product').val('').trigger('change');
            $("#expireDate").val('');
        },
        loadProducts() {
            let self = this;
            axios.get('/ajax-products')
                .then(function (response) {
                    // handle success
                    self.products = response.data;
                    console.log(self.products);
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .finally(function () {
                    // always executed
                });

        },
        saveProductPurchase(){
            let self = this;
            //check form validation
            if (this.isPurchaseFormValid) {
                //make data object
                let purchaseData = {
                    supplierId: $("#supplierId").select2('val'),
                    paidAmount: self.paidAmount,
                    purchaseDate: $("#purchaseDate").val(),
                    internalMemo: self.internalMemo,
                    externalMemo: self.externalMemo,
                    purchaseNote: self.purchaseNote,
                    deliveryNote: self.deliveryNote,
                    otherNote: self.otherNote,
                    selectedProducts: self.selectedProducts,
                    totalPrice: self.totalPrice,
                    totalQuantity: self.totalQuantity

                }
                //send save request
                axios.post('/ajax-save-product-purchase', purchaseData)
                    .then(function (response) {
                        if (response.status === 200) {
                            $('#purchaseForm').submit();
                        }
                        else{
                            alert('Something wrong!!!!');
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                        alert('Something wrong!!!!' + error);
                    });
            }
        },
        isPurchaseFormValid(){
            return true;
        },
        removeSelectedProduct(product, index){
            let self = this;
            //remove product
            self.selectedProducts.splice(index, 1)
            //update total price and quantity
        }
    },
    mounted() {
        let self = this;
        self.products = self.loadProducts();
        $(".select2").select2();
        $('.mydatepicker').datepicker({
            autoclose: true,
            todayHighlight: true
        });
    }
})