<template>
    <div>
        <b-overlay :show="isloading" rounded="sm">
            <b-row>
                <b-col>
                    <Widget title="">
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-input-group prepend="Nro Pedido:">
                                    <b-form-input v-model="model.nroOrder" id="nroOrder" placeholder="Nro Pedido" required :state="validateState('nroOrder')"></b-form-input>
                                    <b-input-group-append>
                                        <b-button variant="info" @click="Search()">Search</b-button>
                                        <b-button variant="outline-primary" @click="Show('modal-new-serial')" v-if="false">New Serial</b-button>
                                        <b-button variant="outline-warning" @click="Show('modal-upload-serial')" v-if="false">Massive load</b-button>
                                    </b-input-group-append>
                                </b-input-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12"><hr /></b-col>
                        </b-row>
                        <b-row v-if="isShow">
                            <b-col>
                                <p><strong>Date:</strong> {{row.dateCreation}}</p>
                                <p> <strong>Nro Orden:</strong> {{row.nroOrder}}</p>
                                <p><strong>Nro Internal:</strong> {{row.line}}</p>
                                <p><strong>Quantity:</strong> {{row.quantity}}</p>
                                <p><strong>Warehouse:</strong> {{row.warehouse}}</p>
                            </b-col>
                        </b-row>
                        <b-row v-if="isShow">
                            <b-col md="12" xl="12" sm="12" xs="12"><div class="p-4"></div></b-col>
                        </b-row>
                        <b-row v-if="isShow">
                            <b-col>
                                <b-tabs>
                                    <b-tab title="Assigned">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <b-button variant="danger" @click="Apply()" v-if="false">Apply</b-button>
                                            </div>
                                        </div>
                                        <div class="table-resposive">
                                            <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Serial</th>
                                                        <th>Phone</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(row, index) in items" :key="index" v-if="row.status=='A'">
                                                        <td>
                                                            <b-button-group>
                                                                <b-button size="sm" variant="danger" @click="Remove(index,row)" v-if="false"><i class="fa fa-remove"></i> </b-button>
                                                                <b-button size="sm" variant="success" @click="ChangeStatus(row, index)">Approved</b-button>
                                                                <b-button size="sm" variant="warning" v-if="false">Update</b-button>
                                                            </b-button-group>
                                                        </td>
                                                        <td>
                                                            <b-form-input v-model="row.serial" id="nroOrder" required :state="validateState('nroOrder')" disabled></b-form-input>
                                                        </td>
                                                        <td>
                                                            <b-form-input v-model="row.phone" id="nroOrder" required :state="validateState('nroOrder')" disabled></b-form-input>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </b-tab>
                                    <b-tab title="Approved">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <b-button variant="danger" @click="Apply()" v-if="false">Apply</b-button>
                                            </div>
                                        </div>
                                        <div class="table-resposive">
                                            <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Serial</th>
                                                        <th>Phone</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(row, index) in items" :key="index" v-if="row.status=='P'">
                                                        <td></td>
                                                        <td>
                                                            <b-form-input v-model="row.serial" id="nroOrder" required :state="validateState('nroOrder')" disabled></b-form-input>
                                                        </td>
                                                        <td>
                                                            <b-form-input v-model="row.phone" id="nroOrder" required :state="validateState('nroOrder')" disabled></b-form-input>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </b-tab>
                                </b-tabs>
                            </b-col>
                        </b-row>
                    </Widget>
                </b-col>
            </b-row>
        </b-overlay>

        <b-modal size="xl" ref="modal-new-serial" hide-footer title="New Serial">
            <b-overlay :show="isloading" rounded="sm">
                <b-card>
                    <b-card-body>
                        <b-row>
                            <b-col md="6" xl="6" sm="6" xs="12">
                                <b-form-group label="Serial"
                                              label-for="Serial-input">
                                    <b-form-input id="Serial-input" v-model="model.serial" required :state="validateState('serial')">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6" xl="6" sm="6" xs="12">
                                <b-form-group label="Phone"
                                              label-for="phone-input">
                                    <b-form-input id="phone-input" typ v-model="model.phone" required :state="validateState('phone')">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <div class="float-right">
                                    <b-button variant="success" id="Save" name="Save" @click="Save()">Save</b-button>
                                </div>
                            </b-col>
                        </b-row>
                    </b-card-body>
                </b-card>
            </b-overlay>
        </b-modal>

        <b-modal size="xl" ref="modal-upload-serial" hide-footer title="Upload Serials">
            <b-overlay :show="isloading" rounded="sm">
                <b-card>
                    <b-card-body>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <form enctype="multipart/form-data">
                                    <b-form-group label-size="sm">

                                        <b-form-file id="file" type="file" ref="file" size="sm"
                                                     placeholder="Choose a file or drop it here..." drop-placeholder="Drop file here..." v-model="file" @change="handleFileUpload($event)"></b-form-file>

                                    </b-form-group>
                                </form>
                            </b-col>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <a href="#">Download Format</a>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <div class="float-right">
                                    <b-button variant="success" id="Upload" name="Upload" @click="Upload()">Upload</b-button>
                                </div>
                            </b-col>
                        </b-row>
                    </b-card-body>
                </b-card>
            </b-overlay>
        </b-modal>
    </div>
</template>

<script>
    import Widget from '@/components/Widget/Widget';
    import { HTTP } from '@/core/Services/http-services';
    import { required } from "vuelidate/lib/validators";
    export default {
        name: 'Sku',
        components: {
            Widget,
        },
        data() {
            return {
                isloading: false,
                isShow: false,
                paginationAndFilter: {
                    pageIndex: 1,
                    pageSize: 10,
                    pageTotals: 1,
                },
                model: {
                    nroOrder: '',
                    serial: '',
                    phone: '',
                    status: 'A',
                },
                items: [],
                row: {},
                file: '',
            }
        },
        validations: {
            model: {
                nroOrder: {
                    required
                },
                serial: {
                    required
                },
                phone: {
                    required
                },
            }

        },
        watch: {

        },
        computed: {

        },
        mounted() {

        },
        methods: {
            validateState(name) {
                const { $dirty, $error } = this.$v.model[name];
                return $dirty ? !$error : null;
            },
            validationModel: function () {
                this.$v.model.$touch();
                if (this.$v.model.$anyError) {
                    this.isloading = false;
                    return;
                }
            },
            Show: function (key) {
                this.$refs[key].show();
            },
            Hide: function (key) {
                this.$refs[key].hide();
            },
            handleFileUpload(event) {
                this.file = event.target.files[0];
            },
            Search: function () {
                this.isloading = true;
                this.validationModel();
                return HTTP.get(`${this.$route.query.url}Order/Get?nroOrden=` + "&nroOrden=" + this.model.nroOrder)
                    .then(response => {
                        this.isloading = false;
                        this.row = response.data.data;
                        this.UploadSku();
                        this.isShow = this.row != null;
                        if (!this.isShow)
                            this.$toasted.info('No data available for this order');

                    })
                    .catch(e => {
                        this.isloading = false;
                        this.$toasted.error(e);
                    });
            },
            Save: function () {
                this.isloading = true;
                this.validationModel();
                return HTTP.post(`${this.$route.query.url}Order/Save`, this.model)
                    .then(response => {
                        this.isloading = false;
                        this.items.push(response.data.data);
                    }).catch(e => {
                        this.isloading = false;
                        this.$toasted.error(e);
                    });;

                this.Hide('modal-new-serial');
            },
            Upload: function () {
                this.isloading = true;
                let formData = new FormData();
                formData.append("file", this.file);
                let config = {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                };
                return HTTP.post(`${this.$route.query.url}Order/Import&nroOrde=` + this.model.nroOrder, formData, config)
                    .then(response => {
                        this.items = response.data.data;
                        this.isloading = false;
                    });
            },
            ChangeStatus: function (row) {
                this.isloading = true;
                this.validationModel();
                return HTTP.post(`${this.$route.query.url}Order/ApprovedSku`, row)
                    .then(response => {
                        this.isloading = false;
                        row.status = 'P';
                        if (!this.isShow)
                            this.$toasted.info('No data available for this order');
                    })
                    .catch(e => {
                        this.isloading = false;
                        this.$toasted.error(e);
                    });

            },
            Remove(index, row) {
                this.isloading = true;
                return HTTP.post(`${this.$route.query.url}Order/Delete&id=` + row.id)
                    .then(response => {
                        this.items.splice(index, 1);
                        this.isloading = false;
                    }).catch(e => {
                        this.isloading = false;
                        this.$toasted.error(e);
                    });;

            },
            Apply(index, tag) {

            },
            UploadSku: function () {
                this.isloading = true;
                this.validationModel();
                return HTTP.get(`${this.$route.query.url}Order/UploadSku&nroOrden=`+ this.model.nroOrder)
                    .then(response => {
                        this.items = response.data.data;
                        this.isloading = false;

                    })
                    .catch(e => {
                        this.isloading = false;
                        this.$toasted.error(e);
                    });
            },

        }
    }


</script>

