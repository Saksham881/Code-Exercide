<template>
    <div class="BookTable">
        <b-table striped hover :items="bookList" :fields="fields" responsive="sm">
            <template v-slot:cell(thumbnailUrl)="data">
                <b-img :src="data.value" thumbnail fluid></b-img>
            </template>
            <template v-slot:cell(title_link)="data">
                <b-link :to="{ name: 'book_view', params: { 'id' : `${data.item.bookId}` } }">{{ data.item.title }}</b-link>
            </template>
            <span v-if="bookList.lenght < 1">No more books in list...</span>
        </b-table>
    </div>
</template>

<script>
    export default {
        name: 'Home',
        props: {
            bookList: Array
        },
        data: () => ({
            fields: [
                { key: 'thumbnailUrl', label: 'Book Image' },
                { key: 'title_link', label: 'Book Title', sortable: true, sortDirection: 'desc' },
                { key: 'isbn', label: 'ISBN', sortable: true, sortDirection: 'desc' },
                { key: 'descr', label: 'Description', sortable: true, sortDirection: 'desc' }
            ],
            items: []
        })
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

