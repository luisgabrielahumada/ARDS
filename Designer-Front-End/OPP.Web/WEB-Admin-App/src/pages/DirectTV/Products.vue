<template>
    <div>
        <b-row>
            <b-col>
                <Widget title="">
                    <div class="tables-basic">
                        <h2 class="page-title"><span class="fw-semi-bold">Catalogo</span></h2>
                        <b-overlay :show="isloading" rounded="sm">
                            <b-row>
                                <b-col md="6" xl="6" sm="6" xs="12">
                                    <b-form-group id="input-group-1" label="Grupo de Inventario:" label-for="input-1">
                                        <b-form-select id="Group" v-model="paginationAndFilter.group" class="mb-2 mr-sm-2 mb-sm-0" text-field="name">
                                            <template #first>
                                                <b-form-select-option :value="0" selected>-- All --</b-form-select-option>
                                                <b-form-select-option v-for="inventoryGroup in inventoryGroups" :value="inventoryGroup.idInventoryGroup">
                                                    {{ inventoryGroup.nameInventoryGroup }}
                                                </b-form-select-option>
                                            </template>
                                        </b-form-select>
                                    </b-form-group>
                                </b-col>
                                <b-col>
                                    <b-form-group id="input-group-1" label="SKU:" label-for="input-1">
                                        <b-form-input id="Sku" v-model="paginationAndFilter.sku" placeholder="SKU"></b-form-input>
                                    </b-form-group>
                                </b-col>
                                <b-col>
                                    <b-form-group id="input-group-1" label="Descripción:" label-for="input-1">
                                        <b-form-input id="Part" v-model="paginationAndFilter.part" placeholder="Parte"></b-form-input>
                                    </b-form-group>
                                </b-col>
                                <b-col>
                                    <b-form-group id="input-group-1" label="--" label-for="input-1">
                                        <b-button variant="primary" @click="Search()">Buscar</b-button>
                                    </b-form-group>
                                </b-col>
                            </b-row>
                            <div>
                                <b-card-group columns>
                                    <b-card no-body class="overflow-hidden" style="max-width: 540px;" v-for="item in items" :key="item.id">
                                        <b-row no-gutters>
                                            <b-col md="6">
                                                <b-card-img :src="getImgUrl(item)" alt="Image" class="rounded-0"></b-card-img>
                                            </b-col>
                                            <b-col md="6">
                                                <b-card-body :title="item.name" :sub-title="item.sku">
                                                    <b-card-text>
                                                        {{item.description}}
                                                        <b-button @click="Detail('Product',item.id)" variant="link">Detail</b-button>
                                                    </b-card-text>
                                                </b-card-body>
                                            </b-col>
                                        </b-row>
                                    </b-card>
                                </b-card-group>
                            </div>
                            <div class="clearfix">
                                <div class="float-right">
                                    <b-pagination v-model="paginationAndFilter.pageIndex"
                                                  :total-rows="paginationAndFilter.pageTotals"
                                                  :per-page="paginationAndFilter.pageSize"
                                                  first-number
                                                  last-number
                                                  @change="List">
                                    </b-pagination>
                                </div>
                            </div>
                        </b-overlay>
                    </div>
                </Widget>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import Widget from '@/components/Widget/Widget';
    import { HTTP } from '@/core/Services/http-services';
    import { required } from "vuelidate/lib/validators";
    export default {
        name: 'Products',
        components: { Widget },
        data() {
            return {
                isloading: false,
                items: [],
                inventoryGroups: [],
                selected: '',
                paginationAndFilter: {
                    pageIndex: 1,
                    pageSize: 50,
                    pageTotals: 1,
                    group: '',
                    sku: '',
                    part: '',
                },
                url: {
                    image: ''
                },
                setting: {
                    image: {
                        url: '',
                        sheet: '',
                    },
                },
            };
        },
        computed: {
        },
        methods: {
            getImgUrl: function (item) {
                return `${this.setting.image.url}${item.image}`;
            },
            List: function (pageIndex) {
                this.isloading = true;
                this.paginationAndFilter.pageIndex = pageIndex;
                return HTTP.post(`${this.$route.query.url}Products/List?token=` + this.$route.query.token, this.paginationAndFilter)
                    .then(response => {
                        this.items = response.data.data.items;
                        this.isloading = false;
                        this.paginationAndFilter.pageTotals = response.data.data.totalItemCount;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
            InventoryGroupList: function () {
                this.isloading = true;
                return HTTP.get(`${this.$route.query.url}Products/GetInventoryGroupList?token=` + this.$route.query.token)
                    .then(response => {
                        this.inventoryGroups = response.data.data;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
            Search: function () {
                this.List(this.paginationAndFilter.pageIndex);
            },
            Detail: function (key, id) {
                this.$router.push(key + "?id=" + id + "&token=" + this.$route.query.token + "&url=" + this.$route.query.url);
            },
            GetSettings: function () {
                this.isloading = true;
                return HTTP.get(`${this.$route.query.url}Products/GetSettings`)
                    .then(response => {
                        this.setting = response.data.data;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
        },
        mounted() {
            this.GetSettings();
            this.InventoryGroupList();
        }
    };
</script>