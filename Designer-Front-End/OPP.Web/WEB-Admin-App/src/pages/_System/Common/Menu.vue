<template>
    <div>
        <h2 class="page-title"><span class="fw-semi-bold">Menus</span></h2>
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
                                        <th>Path</th>
                                        <th>Icon</th>
                                        <th>Root</th>
                                        <th class="hidden-sm-down">Status</th>
                                        <th class="hidden-sm-down">Source Prime</th>
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
                                        <td>
                                            {{row.path}}
                                        </td>
                                        <td>
                                            {{row.icon}}
                                        </td>
                                        <td>
                                            {{row.root}}
                                        </td>
                                        <td class="width-150">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="row.status" value="true" unchecked-value="false" disabled  switch></b-form-checkbox>
                                        </td>
                                        <td class="width-150">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="row.sourcePrime" value="true" unchecked-value="false" disabled  switch></b-form-checkbox>
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
        <b-modal size="xl" ref="modal-menu" hide-footer>
            <template #modal-title>
                <b>{{model.name}}</b>
            </template>
            <b-overlay :show="isloading" rounded="sm">
                <b-card>
                    <b-card-body>
                        <b-row>
                            <b-col md="4" xl="4" sm="4" xs="12">
                                <b-form-group id="input-group-1" label="Name:" label-for="input-1">
                                    <b-form-input id="input-1" v-model="model.name" :state="validateState('name')" type="text" placeholder="Enter Name" required></b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4" xl="4" sm="4" xs="12">
                                <b-form-group id="input-group-2" label="Path:" label-for="input-2">
                                    <b-form-input id="input-2" v-model="model.path" :state="validateState('path')" type="text" placeholder="Enter Path" required></b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4" xl="4" sm="4" xs="12">
                                <b-form-group id="input-group-3" label="Icon:" label-for="input-3">
                                    <b-form-input id="input-3" v-model="model.icon" type="text" placeholder="Enter Icon" required></b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" xl="6" sm="6" xs="12">
                                <b-form-group id="input-group-4" label="Root:" label-for="input-4">
                                    <!--<b-form-input id="input-4" v-model="model.root" type="text" placeholder="Enter Root" required></b-form-input>-->
                                    <b-form-select v-model="model.root" class="mb-3" :state="validateState('root')">
                                        <template slot="first">
                                            <option :value="0" selected>-- Not Parents --</option>
                                        </template>
                                        <option v-for="root in rootMenu" v-bind:value="root.id" :selected="model.root===root.id">
                                            {{ root.id }} - {{ root.name }}
                                        </option>
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                            <b-col md="6" xl="6" sm="6" xs="12" class="pt-5">
                                <b-row>
                                    <b-col md="6" xl="6" sm="6" xs="12">
                                        <b-form-group id="input-group-5" label="" label-for="input-5">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="model.status"  switch>Status</b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6" xl="6" sm="6" xs="12">
                                        <b-form-group id="input-group-6" label="" label-for="input-6">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="model.sourcePrime"   switch>Soure Prime</b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row v-if="model.id !=0">
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-card no-body>
                                    <b-tabs>
                                        <b-tab title="Assign Permissions" active>
                                            <Permissions :id="model.id" :items="itemsPermissions" :roles="roleItems"></Permissions>
                                        </b-tab>
                                    </b-tabs>
                                </b-card>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12"><hr /></b-col>
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
    import Permissions from './Shared/Permissions';
    export default {
        name: 'Menus',
        components: { Widget, Permissions },
        props: {
            dataBase: [String, Number]
        },
        data() {
            return {
                isloading: false,
                items: [],
                rootMenu: [],
                itemsPermissions: [],
                paginationAndFilter: {
                    pageIndex: 1,
                    pageSize: 10,
                    pageTotals: 1,
                    dataBaseId: 0
                },
                model: {
                    id: 0,
                    name: 'New Menu',
                    path: '',
                    icon: '',
                    root: 0,
                    status: true,
                    dataBaseId: 0,
                    sourcePrime: false
                },
            };
        },
        validations: {
            model: {
                name: {
                    required
                },
                path: {
                    required
                },
                root: {
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
                this.paginationAndFilter.dataBaseId = this.dataBase;
                return HTTP.post('Menu/List', this.paginationAndFilter)
                    .then(response => {
                        this.items = response.data.data.items;
                        this.paginationAndFilter.pageTotals = response.data.data.totalItemCount;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data.message);
                        this.isloading = false;
                    });
            },
            Get: function (id) {
                id = id != 0 ? id : this.model.id;
                if (id === 0) {
                    this.Reset();
                    this.$refs['modal-menu'].show();
                    return;
                };
                this.model.id = id;
                return HTTP.post('Menu/Get', { id: id, databaseId: this.dataBase })
                    .then(response => {
                        this.model = response.data.data.row[0];
                        this.itemsPermissions = response.data.data.items;
                        this.$refs['modal-menu'].show();
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });
            },
            GetRoot: function () {
                return HTTP.post('Menu/GetRoot', { databaseId: this.dataBase })
                    .then(response => {
                        this.rootMenu = response.data.data;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });
            },
            GetRole: function () {
                const pagination = {
                    pageIndex: 1,
                    pageSize: 9999999,
                    dataBaseId: this.dataBase
                };
                return HTTP.post('Role/List', pagination)
                    .then(response => {
                        this.roleItems = response.data.data.items;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });
            },
            Save: function () {
                this.isloading = true;
                this.$v.model.$touch();
                if (this.$v.model.$anyError) {
                    return;
                }
                this.model.dataBaseId = this.dataBase;
                return HTTP.post('Menu/Save', this.model)
                    .then(response => {
                        this.$toasted.success('Successful process.');
                        this.List(this.paginationAndFilter.pageIndex);
                        this.isloading = false;
                        //this.$refs['modal-menu'].hide();
                        this.model.id = response.data.data;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                        this.isloading = false;
                    });
            },
            Reset: function () {
                this.model.id = 0;
                this.model.name = 'New Menu';
                this.model.path = '';
                this.model.icon = '';
                this.model.root = '';
            }
        },
        mounted() {
            this.List(this.paginationAndFilter.pageIndex);
            this.GetRoot();
            this.GetRole();
        }
    };
</script>
