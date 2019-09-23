
var purchsePage = new Vue({
    el: '#salePage',
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
        saleNote: null,
        totalPrice:0,
        totalQuantity: 0,
        discount: 0,
        payable: 0,
        changeAmount: 0,
        customerId: null,
        saleDate: null,
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
                    self.totalPrice += parseInt(product.price);
                    self.totalQuantity += parseInt(product.quantity);
                });
                self.clearField();
            }
        },
        isValid() {
            let self = this;
            let isvalid = false;
            self.clearError();
            // self.product = $("#product").select2('val');
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
            axios.get('/ajax-sale-products')
                .then(function (response) {
                    // handle success
                    console.log(response.data);
                    self.products = response.data;
                })
                .catch(function (error) {
                    // handle error
                    console.log(error);
                })
                .finally(function () {
                    // always executed
                });

        },
        saveProductSales(){
            let self = this;
            //check form validation
            if (this.isSaleFormValid) {
                //make data object
                let saleData = {
                    selectedProducts: self.selectedProducts,
                    totalQuantity: self.totalQuantity,
                    totalPrice: self.totalPrice,
                    paidAmount: self.paidAmount,
                    discount: self.discount,
                    customerId: $("#customerId").select2('val'),
                    saleData: $("#saleData").val(),
                    internalMemo: self.internalMemo,
                    externalMemo: self.externalMemo,
                    saleNote: self.saleNote,
                }
                //send save request
                axios.post('/ajax-save-product-sale', saleData)
                    .then(function (response) {
                        if (response.status === 200) {
                            // $('#purchaseForm').submit();
                            window.location.replace('/ProductSale');
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
        isSaleFormValid(){
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

        $('#product').on("select2:closing", function (e) {
            // what you would like to happen
            console.log(e);
            let index = $("#product").select2('val');
            let selectedProduct = self.products[index];
            self.product = selectedProduct.id;
            self.price = selectedProduct.price;
            $("#expireDate").val(selectedProduct.expireDate);
            self.remainingQuantity = selectedProduct.remainingQuantity;
        });
    }
})