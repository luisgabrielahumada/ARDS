<template>
    <div>
        <h2 class="page-title"><span class="fw-semi-bold">Roles</span></h2>
        <b-overlay :show="isloading" rounded="sm">
            <b-row>
                <b-col>
                    <Widget title="">
                        <div class="clearfix">
                            <div class="float-right">
                                <b-dropdown variant="inverse" class="mr-xs" size="sm" text="Options" right>
                                    <b-dropdown-item @click="Get(0)">New</b-dropdown-item>
                                    <b-dropdown-item @click="List(paginationAndFilter.pageIndex)">Refresh</b-dropdown-item>
                                </b-dropdown>
                            </div>
                        </div>
                        <div class="table-resposive">
                            <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                                <thead>
                                    <tr>
                                        <th class="hidden-sm-down">#</th>
                                        <th>Name</th>
                                        <th class="hidden-sm-down">Status</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="row in items" :key="row.id">
                                        <td>
                                            <a class="d-flex sidebar-link" @click="Get(row.id)">
                                                <span class="glyphicon glyphicon-edit">
                                                </span>
                                            </a>
                                        </td>
                                        <td>
                                            {{row.name}}
                                        </td>
                                        <td class="width-150">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="row.status" disabled  switch></b-form-checkbox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
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
                    </Widget>
                </b-col>
            </b-row>
        </b-overlay>

        <b-modal size="xl" ref="modal-role" hide-footer>
            <template #modal-title>
                <b>{{model.name}}</b>
            </template>
            <b-overlay :show="isloading" rounded="sm">
                <b-card>
                    <b-card-body>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-form-group id="input-group-1" label="Name:" label-for="input-1">
                                    <b-form-input id="input-1" v-model="model.name" :state="validateState('name')" type="text" placeholder="Enter Name" required></b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-form-group id="input-group-2" label="" label-for="input-2">
                                    <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="model.status" value="true" unchecked-value="false" switch>Status</b-form-checkbox>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <div class="float-right">
                                    <b-button variant="success" id="Save" @click="Save">Save</b-button>
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
        name: 'Roles',
        components: { Widget },
        props: {
            dataBase: [String, Number]
        },
        data() {
            return {
                isloading: false,
                items: [],
                paginationAndFilter: {
                    pageIndex: 1,
                    pageSize: 10,
                    pageTotals: 1,
                    dataBaseId: 0
                },
                model: {
                    id: 0,
                    name: 'New Role',
                    status: true
                }
            };
        },
        validations: {
            model: {
                name: {
                    required
                },
            }
        },
        methods: {
            validateState(name) {
                const { $dirty, $error } = this.$v.model[name];
                return $dirty ? !$error : null;
            },
            List: function (pageIndex) {
                this.isloading = true;
                this.paginationAndFilter.pageIndex = pageIndex;
                this.paginationAndFilter.dataBaseId = Number(this.dataBase) ;
                return HTTP.post('Role/List', this.paginationAndFilter)
                    .then(response => {
                        this.items = response.data.data.items;
                        this.paginationAndFilter.pageTotals = response.data.data.totalItemCount;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                        this.isloading = false;
                    });
            },
            Get: function (id) {
                if (id === 0) {
                    this.Reset();
                    this.$refs['modal-role'].show();
                    return;
                };
                return HTTP.get('Role/Get?id=' + id)
                    .then(response => {
                        this.model = response.data.data;
                        this.$refs['modal-role'].show();
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });

            },
            Save: function () {
                this.isloading = true;
                this.$v.model.$touch();
                if (this.$v.model.$anyError) {
                    this.isloading = false;
                    return;
                }
                return HTTP.post('Role/Save', this.model)
                    .then(response => {
                        this.$toasted.success('Successful process.');
                        this.List(this.paginationAndFilter.pageIndex);
                        this.isloading = false;
                        this.$refs['modal-role'].hide();
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                        this.isloading = false;
                    });
            },
            Reset: function () {
                this.model.id = 0;
                this.model.name = 'New Role';
            }
        },
        mounted() {
            this.List(this.paginationAndFilter.pageIndex);
        }
    };
</script>
