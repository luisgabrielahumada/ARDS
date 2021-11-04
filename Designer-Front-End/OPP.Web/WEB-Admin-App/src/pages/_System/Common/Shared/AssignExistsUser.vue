<template>
    <div>
        <b-row>
            <b-col md="12" xl="12" sm="12" xs="12">
                <b-form-group label="User select" label-for="tags-component-select">
                    <b-form-select :options="availableitems"
                                   value-field="id"
                                   text-field="name"
                                   :disabled="availableitems.length === 0" @change="AddUser">
                        <template #first>
                            <option disabled value="0" selected>Choose a tag...</option>
                        </template>
                    </b-form-select>
                    <div v-for='(user, index) in selectedUser' :key='index' class='tag-input__tag'>
                        <span @click='RemoveUser(index,user)'>x</span>
                        {{ user.name }}
                    </div>
                </b-form-group>
            </b-col>
        </b-row>
        <b-row>
            <b-col md="12" xl="12" sm="12" xs="12">
                <div class="float-right">
                    <b-button variant="success" id="Save" @click="Save" :disabled="!activeButton">Save</b-button>
                </div>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { HTTP } from '@/core/Services/http-services';
    export default {
        name: 'AssignExistsUser',
        components: { HTTP },
        props: {
            id: String,
            users: Array,
            selected: Array
        },
        data() {
            return {
                selectedUser: [],
                items: [],
                itemsTemp: [],
                model: {
                    dataBaseId: 0,
                    users: []
                },
            };
        },
        computed: {
            activeButton() {
                return (this.selectedUser.length > 0);
            },
            availableitems() {
                this.itemsTemp = [];
                this.items.filter((value, index) => {
                    const item = this.selectedUser.some(function (field) {
                        let r = false;
                        if (field.id === value.id) {
                            r = true;
                            return r;
                        }
                    });
                    if (!item) {
                        const i = this.items.find(m => m.id === value.id);
                        this.itemsTemp.push(i);
                    }
                });
                return this.itemsTemp;
            }
        },
        methods: {
            RemoveUser(index, tag) {
                this.selectedUser.splice(index, 1)
            },
            AddUser(event) {
                var item = this.itemsTemp.find(m => m.id === event);
                this.selectedUser.push({ id: item.id, name: item.name });
                this.itemsTemp.splice(item, 1);
            },
            Save() {
                this.model.users = this.selectedUser;
                return HTTP.post('User/SaveExistsUsers', this.model)
                    .then(response => {
                        this.$toasted.success('Successful process.');
                        this.getUsers();

                    })
                    .catch(e => {
                        this.$toasted.error(e);
                    });
            },
            getUsers() {
                this.$emit('get-Users');
            }
        },
        mounted() {
            this.items = this.users;
            this.selectedUser = this.selected;
            this.model.dataBaseId = this.id;
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