<template>
    <div>
        <b-row>
            <b-col>
                <Widget title="">
                    <b-form @submit.stop.prevent="Save">
                        <b-overlay :show="isloading" rounded="sm">
                            <b-row>
                                <b-col md="6" xl="6" sm="6" xs="12">
                                    <b-form-group id="input-group-1" label="Name:" label-for="input-1">
                                        <b-form-input id="name" name="name" v-model="model.name" :state="validateState('name')" type="text" placeholder="Enter Name" required></b-form-input>
                                    </b-form-group>
                                </b-col>
                                <b-col md="6" xl="6" sm="6" xs="12">
                                    <b-form-group id="input-group-2" label="Alias:" label-for="input-2">
                                        <b-form-input id="input-2" v-model="model.alias" type="text" :state="validateState('alias')" placeholder="Enter Alias" required></b-form-input>
                                    </b-form-group>
                                </b-col>
                            </b-row>

                            <b-row>
                                <b-col md="12" xl="12" sm="12" xs="12">
                                    <b-form-group id="input-group-3" label="Connection String:" label-for="input-3">
                                        <b-form-input id="input-3" v-model="model.connectionString" :state="validateState('connectionString')" type="text" placeholder="Enter Connection String" required></b-form-input>
                                    </b-form-group>
                                </b-col>
                                <b-col md="12" xl="12" sm="12" xs="12">
                                    <b-form-group id="input-group-4" label="Storage:" label-for="input-4">
                                        <b-form-input id="input-4" v-model="model.storage" type="text" placeholder="Enter Storage"></b-form-input>
                                    </b-form-group>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col md="12" xl="12" sm="12" xs="12">
                                    <b-form-group id="input-group-4" label="" label-for="input-4">
                                        <b-form-checkbox class="mb-2 mr-sm-2 mb-sm-0" v-model="model.status" value="true" unchecked-value="false" switch>Status</b-form-checkbox>
                                    </b-form-group>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col md="12" xl="12" sm="12" xs="12">
                                    <div class="float-right">
                                        <b-button variant="danger" id="Cancel" @click="Cancel">Cancel</b-button>&#160;
                                        <b-button variant="success" id="Save" @click="Save">Save</b-button>
                                    </div>
                                </b-col>
                            </b-row>
                        </b-overlay>
                    </b-form>
                    <b-row>
                        <b-col md="12" xl="12" sm="12" xs="12"><hr /></b-col>
                    </b-row>
                    <b-row v-if="dataBaseId!=0">
                        <b-col md="12" xl="12" sm="12" xs="12">
                            <b-card no-body>
                                <b-tabs>
                                    <b-tab title="Roles">
                                        <Role :data-base="dataBaseId"></Role>
                                    </b-tab>
                                    <b-tab title="Users">
                                        <User :data-base="dataBaseId"></User>
                                    </b-tab>
                                    <b-tab title="Menus">
                                        <Menu :data-base="dataBaseId"></Menu>
                                    </b-tab>
                                    <b-tab title="Upload File">
                                        <upload-File :id="$route.query.id" :tablename="'DataBasesObject'" ref="uploadFile"></upload-File>
                                    </b-tab>
                                </b-tabs>
                            </b-card>
                        </b-col>
                    </b-row>
                </Widget>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import Widget from '@/components/Widget/Widget';
    import { HTTP } from '@/core/Services/http-services';
    import { required } from "vuelidate/lib/validators";
    import Role from './Common/Role';
    import User from './Common/User';
    import Menu from './Common/Menu';
    import uploadFile from './Common/Shared/UploadFile';
    export default {
        name: 'DataBase',
        components: {
            Widget,
            Role,
            User,
            Menu,
            uploadFile
        },
        computed: {

        },
        data() {
            return {
                isloading: false,
                dataBaseId: 0,
                show: true,
                items: [],
                paginationAndFilter: {
                    pageIndex: 1,
                    pageSize: 10,
                    pageTotals: 1,
                },
                model: {
                    id: 0,
                    name: '',
                    alias: '',
                    connectionString: '',
                    storage: '',
                    status: false,
                },
                errors: [],
            };
        },
        validations: {
            model: {
                name: {
                    required
                },
                alias: {
                    required
                },
                connectionString: {
                    required
                },
            }
        },
        methods: {
            validateState(name) {
                const { $dirty, $error } = this.$v.model[name];
                return $dirty ? !$error : null;
            },
            Cancel: function (id) {
                this.$router.push('/system/configuration/data-bases');
            },
            Get: function (id) {
                if (id === "0") return;
                this.isloading = true;
                return HTTP.get('DataBase/Get?id=' + id)
                    .then(response => {
                        this.model = response.data.data;
                        this.isloading = false;
                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                        this.isloading = false;
                    });
            },
            Save: function (e) {
                this.isloading = true;
                this.$v.model.$touch();
                if (this.$v.model.$anyError) {
                    this.isloading = false;
                    return;
                }
                return HTTP.post('DataBase/Save', this.model)
                    .then(response => {
                        this.dataBaseId = response.data.data;
                        this.$toasted.success('Successful process.');
                        //this.$emit('saveFile', this.dataBaseId);
                        this.$refs.uploadFile.submitFile(this.dataBaseId, "DataBasesObject");

                        if (this.$route.query.id == 0)
                            this.$router.push('/system/configuration/data-base?id=' + this.dataBaseId);
                        this.Get(this.dataBaseId);
                    })
                    .catch(e => {
                        this.$toasted.error(e);
                        this.isloading = false;
                    });
            },
        },
        mounted() {
            this.dataBaseId = this.$route.query.id;
            this.Get(this.dataBaseId);
        }
    };
</script>
