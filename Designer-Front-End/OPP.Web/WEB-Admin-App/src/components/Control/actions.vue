<template>
    <div>
        <b-dropdown size="lg" variant="link" toggle-class="text-decoration-none" no-caret>
            <template #button-content>
                <span class="fa fa-sliders"></span>
            </template>
            <b-dropdown-item href="#" v-for='(item, index) in items' :key='index' @click="Apply(item)" ref="item.refModal"><span :class="item.icon"></span> {{item.name}}</b-dropdown-item>
        </b-dropdown>
    </div>
</template>
<script>
    import { HTTP } from '@/core/Services/http-services';
    export default {
        name: 'Actions',
        props: {
            id: [String, Number],
            code: String
        },
        data() {
            return {
                items: [
                    {
                        name: "Opt 1",
                        icon: "fa fa-cog",
                        id: 0,
                        ismodal: false,

                    },
                    {
                        name: "Opt 2",
                        icon: "fa fa-cog",
                        id: 0,
                        ismodal: false
                    },
                    {
                        name: "Opt 3",
                        icon: "fa fa-cog",
                        id: 0,
                        ismodal: true,
                        refModal: "modal-Pruebas"
                    }
                ],
            }
        },
        watch: {

        },
        methods: {
            Get() {
                return HTTP.get('Actions/Get?code=' + code)
                    .then(response => {
                        this.items = response.data.data
                    })
                    .catch(e => {
                    })
            },
            Apply(item) {
                if (item.ismodal) {
                    this.$root.$emit('bv::show::modal', item.refModal, '#' + item.refModal);
                    return;
                }
                return HTTP.post('Actions/Apply', { item })
                    .then(response => {
                        this.$toasted.success("remove file successfully.");
                    })
                    .catch(e => {
                        console.log(e);
                        this.$toasted.error(e);
                    })

            }
        },
        created: function () {
        },
        mounted() {
        }
    };
</script>
<style></style>
