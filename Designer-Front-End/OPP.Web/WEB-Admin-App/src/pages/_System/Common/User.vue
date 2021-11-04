<template>
    <div>
        <h2 class="page-title"><span class="fw-semi-bold">Users</span></h2>
        <b-overlay :show="isloading" rounded="sm">
            <b-row>
                <b-col>
                    <Widget title="">
                        <div class="clearfix">
                            <div class="float-right">
                                <b-dropdown variant="inverse" class="mr-xs" size="sm" text="Options" right>
                                    <b-dropdown-item @click="Get(0)">New</b-dropdown-item>
                                    <b-dropdown-item @click="AddUser()">Assign Existing User</b-dropdown-item>
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
                                        <th>Login</th>
                                        <th>Email</th>
                                        <th>Document</th>
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
                                        <td>
                                            {{row.login}}
                                        </td>
                                        <td>
                                            {{row.email}}
                                        </td>
                                        <td>
                                            {{row.document}}
                                        </td>
                                        <td class="width-150">
                                            <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="row.status" value="true" unchecked-value="false" disabled  switch></b-form-checkbox>
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
        <b-modal size="xl" ref="modal-user" hide-footer>
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
                                <b-form-group id="input-group-2" label="Login:" label-for="input-2">
                                    <b-form-input id="input-2" v-model="model.login" :state="validateState('login')" type="text" placeholder="Enter Login" required></b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4" xl="4" sm="4" xs="12" v-if="model.id==0">
                                <b-form-group id="input-group-3" label="Password:" label-for="input-3">
                                    <b-form-input id="input-3" v-model="model.password" :state="validateState('password')" type="password" placeholder="Enter Password" required></b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="4" xl="4" sm="4" xs="12">
                                <b-form-group id="input-group-4" label="Email:" label-for="input-4">
                                    <b-form-input id="input-4" v-model="model.email" :state="validateState('email')" type="text" placeholder="Enter Email" required></b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4" xl="4" sm="4" xs="12">
                                <b-form-group id="input-group-5" label="Document:" label-for="input-5">
                                    <b-form-input id="input-5" v-model="model.document" type="text" placeholder="Enter Document" required></b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4" xl="4" sm="4" xs="12" class="pt-sm-5">
                                <b-form-group id="input-group-6" label="" label-for="input-6">
                                    <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="model.status" value="true" unchecked-value="false" switch>Status</b-form-checkbox>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" xl="12" sm="12" xs="12">
                                <b-form-group label="Role select" label-for="tags-component-select">
                                    <b-form-select :options="availableOptions"
                                                   value-field="id"
                                                   text-field="name"
                                                   :disabled="availableOptions.length === 0" @change="AddTag">
                                        <template #first>
                                            <option disabled value="" selected>Choose a tag...</option>
                                        </template>
                                    </b-form-select>
                                    <div v-for='(tag, index) in selectedRole' :key='index' class='tag-input__tag'>
                                        <span @click='RemoveTag(index,tag)'>x</span>
                                        {{ tag.name }}
                                    </div>
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

        <b-modal size="xl" ref="modal-exists-user" hide-footer>
            <template #modal-title>
                <b>{{model.name}}</b>
            </template>
            <b-card>
                <b-card-body>
                    <Assign-Exists-User :id="dataBase" :users="userItems" :selected="userSelected" @get-Users="getUsers"></Assign-Exists-User>
                </b-card-body>
            </b-card>
        </b-modal>

    </div>
</template>

<script>
    import Widget from '@/components/Widget/Widget';
    import { HTTP } from '@/core/Services/http-services';
    import { required } from "vuelidate/lib/validators";
    import AssignExistsUser from './Shared/AssignExistsUser';
    export default {
        name: 'Users',
        components: { Widget, AssignExistsUser },
        props: {
            dataBase: [String, Number]
        },
        data() {
            return {
                isloading: false,
                items: [],
                roleItems: [],
                selectedRole: [],
                roleItemstemp: [],
                userSelected: [],
                userItems: [],
                paginationAndFilter: {
                    pageIndex: 1,
                    pageSize: 10,
                    pageTotals: 1,
                    dataBaseId: 0,
                    all: 0
                },
                model: {
                    id: 0,
                    name: 'New User',
                    login: '',
                    password: '',
                    email: '',
                    status: 0,
                    document: '',
                    dataBaseId: 0,
                    roles: []
                },
            };
        },
        validations: {
            model: {
                name: {
                    required
                },
                login: {
                    required
                },
                password: {
                    required
                },
                email: {
                    required
                },
            }
        },
        computed: {
            availableOptions() {
                this.roleItemstemp = [];
                this.roleItems.filter((value, index) => {
                    const item = this.selectedRole.some(function (field) {
                        let r = false;
                        if (field.id === value.id) {
                            r = true;
                            return r;
                        }
                    });
                    if (!item) {
                        const i = this.roleItems.find(m => m.id === value.id);
                        this.roleItemstemp.push(i);
                    }
                });
                return this.roleItemstemp;
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
                this.paginationAndFilter.all = 0;
                return HTTP.post('User/List', this.paginationAndFilter)
                    .then(response => {
                        this.items = response.data.data.items
                        this.paginationAndFilter.pageTotals = response.data.data.totalItemCount;
                        this.isloading = false;
                        this.$refs['modal-exists-user'].hide()
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                        this.isloading = false;
                    });
                this.GetRole();
            },
            Get: function (id) {
                if (id === 0) {
                    this.Reset();
                    this.selectedRole = [];
                    this.$refs['modal-user'].show();
                    return;
                };
                this.GetRole();
                return HTTP.get('User/Get?id=' + id)
                    .then(response => {
                        this.model = response.data.data.row[0];
                        this.selectedRole = response.data.data.role;
                        this.model.dataBaseId = this.dataBase;
                        this.GetUser();
                        this.$refs['modal-user'].show();
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
                this.model.role = this.selectedRole;
                return HTTP.post('User/Save', this.model)
                    .then(response => {
                        this.$toasted.success('Successful process.');
                        this.List(this.paginationAndFilter.pageIndex);
                        this.isloading = false;
                        this.$refs['modal-user'].hide()
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                        this.isloading = false;
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
            GetUser: function () {
                this.paginationAndFilter.all = 1;
                return HTTP.post('User/ListAll', this.paginationAndFilter)
                    .then(response => {
                        this.userItems = response.data.data.list;
                        this.userSelected = response.data.data.userSelected;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });
            },
            Reset: function () {
                this.model.id = 0;
                this.model.name = 'New User';
                this.model.login = '';
                this.model.password = '';
                this.model.email = '';
                this.model.document = '';
                this.model.document = '';
            },
            RemoveTag(index, tag) {
                this.selectedRole.splice(index, 1)
                //this.roleItems.push({ id: tag.id, name: tag.name });
            },
            AddTag(event) {
                var item = this.roleItems.find(m => m.id === event);
                this.selectedRole.push({ id: item.id, name: item.name });
                this.roleItems.splice(item, 1);
            },
            AddUser: function (id) {
                this.model.name = "Add Users";
                this.$refs['modal-exists-user'].show();
                return;
            },
            getUsers() {
                this.List(this.paginationAndFilter.pageIndex);
            }
        },
        mounted() {
            this.List(this.paginationAndFilter.pageIndex);
            this.GetRole();
            this.GetUser();
        }
    };
</script>
<style scoped>
    .tag-input {
        width: 100%;
        border: 1px solid #eee;
        font-size: 0.9em;
        height: 50px;
        box-sizing: border-box;
        padding: 0 10px;
    }

    .tag-input__tag {
        height: 30px;
        float: left;
        margin-right: 10px;
        background-color: #143cc4;
        color: #ffffff;
        margin-top: 10px;
        line-height: 30px;
        padding: 0 5px;
        border-radius: 5px;
    }

        .tag-input__tag > span {
            cursor: pointer;
            opacity: 0.75;
        }

    .tag-input__text {
        border: none;
        outline: none;
        font-size: 0.9em;
        line-height: 50px;
        background: none;
    }
</style>