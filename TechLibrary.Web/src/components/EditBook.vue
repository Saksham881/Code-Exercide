<template>
    <div>
        <div v-if="book">
        <b-card :img-src="bookDetails.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem;"
                class="mb-2">
            <b-form-input type="text" v-model="bookDetails.thumbnailUrl" placeholder="Edit thumbnail url..."></b-form-input>
            <br>
            <b-form-input type="text" v-model="bookDetails.title" placeholder="Edit tilte..."></b-form-input>
            <br>
            <b-form-textarea rows="1" max-rows="3" v-model="bookDetails.descr" placeholder="Edit description..."></b-form-textarea>
            <br>
            <b-button variant="primary" v-on:click="saveClicked">Save</b-button>
            &nbsp; 
            <b-button variant="primary" v-on:click="cancelClicked">Cancel</b-button>
        </b-card>
    </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'EditBook',
        props: {
            id: String,
            book: Object
        },
        data: () => ({
            bookDetails: {
                bookId: "",
                title: "",
                isbn: "",
                publishedDate: "",
                thumbnailUrl: "",
                descr: ""
            }
        }),
        mounted() {
            axios.get(`https://localhost:5001/books/${this.id}`)
                .then(response => {
                    this.bookDetails = {...response.data};
                });
        },
        methods: {
            cancelClicked() {
                this.$router.push({ name: 'book_view', params: { id : this.id } });
            },
            saveClicked() {
                axios.put(`https://localhost:5001/books`, this.bookDetails)
                    .then((response) => this.$router.push({ name: 'book_view', params: { 'id' : response.data.bookId } } )
                    );
            }
        }
    }
</script>