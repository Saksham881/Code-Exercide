<template>
    <div>
        <div>
        <b-card :img-src="bookDetails.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem;"
                class="mb-2">
            <label for="example-input">Title</label>
            <b-form-input type="text" v-model="bookDetails.title" placeholder="Tilte..."></b-form-input>
            <br>
            <label for="example-input">Descirption</label>
            <b-form-textarea rows="1" max-rows="3" v-model="bookDetails.descr" placeholder="Description..."></b-form-textarea>
            <br>
            <label for="example-input">Thumbnail Url</label>
            <b-form-input type="text" v-model="bookDetails.thumbnailUrl" placeholder="Thumbnail url..."></b-form-input>
            <br>
            <label for="example-input">Publish Date</label>
            <b-form-input type="text" v-model="bookDetails.publishedDate" placeholder="YYYY-MM-DD"></b-form-input>
            <br>
            <b-button variant="primary" v-on:click="saveClicked">Save</b-button>
        </b-card>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'NewBook',
        data: () => ({
            bookDetails: {
                title: "",
                isbn: "",
                publishedDate: "",
                thumbnailUrl: "",
                descr: ""
            }
        }),
        methods: {
            saveClicked() {
                axios.post(`https://localhost:5001/books`, this.bookDetails)
                    .then((response) => {
                        this.$router.push({ name: 'book_view', params: { id: response.data }})
                    });
            }
        }
    }
</script>