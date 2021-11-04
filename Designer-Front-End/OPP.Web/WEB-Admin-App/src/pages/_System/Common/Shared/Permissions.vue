<template>
    <div>
        <b-row>
            <b-col>
                <table class="table table-striped table-bordered table-lg mb-0 requests-table">
                    <thead>
                        <tr>
                            <th>Role</th>
                            <th>Permissions</th>
                            <th class="hidden-sm-down">Status</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <b-form-select :options="roles"
                                               value-field="id"
                                               text-field="name" v-model="role">
                                    <template #first>
                                        <option disabled value="0" selected>Select...</option>
                                    </template>
                                </b-form-select>
                            </td>
                            <td>
                                <b-form-checkbox-group v-model="actions"
                                                       :options="options"
                                                       switches></b-form-checkbox-group>
                            </td>
                            <td class="width-150">
                                <b-form-checkbox id="checkbox-1"
                                                 v-model="status"
                                                 name="checkbox-1"  switch>Status</b-form-checkbox>
                            </td>
                            <td>
                                <b-button variant="danger" id="Add" @click="Add" :disabled="!activeButton">Add/Update</b-button>
                            </td>
                        </tr>
                        <tr v-for="row in items2" :key="row.id">
                            <td>
                                <strong>{{row.name}}</strong>
                            </td>
                            <td>
                                <table class="table table-striped table-lg mb-0 requests-table no-border">
                                    <tr>
                                        <td>
                                            <b-form-checkbox id="checkbox-l"
                                                             v-model="row.l"
                                                             name="checkbox-1" value="1" unchecked-value="0" switch disabled>L</b-form-checkbox>
                                        </td>
                                        <td>
                                            <b-form-checkbox id="checkbox-v"
                                                             v-model="row.v"
                                                             name="checkbox-1" value="1" unchecked-value="0"  switch disabled>V</b-form-checkbox>
                                        </td>
                                        <td>
                                            <b-form-checkbox id="checkbox-w"
                                                             v-model="row.w"
                                                             name="checkbox-1" value="1" unchecked-value="0" switch disabled>W</b-form-checkbox>
                                        </td>
                                        <td>
                                            <b-form-checkbox id="checkbox-r"
                                                             v-model="row.r"
                                                             name="checkbox-1" value="1" unchecked-value="0"  switch disabled>R</b-form-checkbox>
                                        </td>
                                        <td>
                                            <b-form-checkbox id="checkbox-d"
                                                             v-model="row.d"
                                                             name="checkbox-1" value="1" unchecked-value="0"  switch disabled>D</b-form-checkbox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="width-150">
                                <b-form-checkbox id="checkbox-1"
                                                 v-model="row.status"
                                                 name="checkbox-1" value="1" unchecked-value="0"  switch disabled>Status</b-form-checkbox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { HTTP } from '@/core/Services/http-services';
    export default {
        name: 'Permissions',
        components: {},
        props: {
            id: Number,
            items: Array,
            roles: Array
        },
        data() {
            return {
                items2: [],
                actions: [],
                options: [
                    { text: 'L', value: 'L' },
                    { text: 'V', value: 'V' },
                    { text: 'W', value: 'W' },
                    { text: 'R', value: 'R' },
                    { text: 'D', value: 'D' },
                ],
                role: 0,
                status: false
            };
        },
        computed: {
            activeButton() {
                return (this.role > 0 && this.actions.length > 0);
            },
        },
        methods: {
            Add: function () {
                const data = {
                    name: this.roles.find(m => m.id == this.role).name,
                    role: this.role,
                    menu: this.id,
                    l: this.actions.some(m => m == 'L')==true ? 1 :0,
                    v: this.actions.some(m => m == 'V') == true ? 1 : 0,
                    w: this.actions.some(m => m == 'W') == true ? 1 : 0,
                    r: this.actions.some(m => m == 'R') == true ? 1 : 0,
                    d: this.actions.some(m => m == 'D') == true ? 1 : 0,
                    status: this.status == true ? 1 : 0,
                };
                return HTTP.post('Menu/SaveAccess', data)
                    .then(response => {
                        this.$toasted.success('Successful process.');
                        this.items2.filter((value, index) => {
                            
                            if (value.menu == data.menu && value.role == data.role) {
                                this.items2.splice(index,1);
                            }
                        });
                        this.items2.push(data);

                    })
                    .catch(e => {
                        this.$toasted.error(e.response.data);
                    });
            },
            Reset: function () {
                this.actions = [];
                this.role = 0;
            },
            Edit: function () { }
        },
        mounted() {
            this.items2 = this.items;
        }
    };
</script>
