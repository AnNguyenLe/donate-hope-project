import React, { useCallback, useEffect, useState } from "react";
import {
    TextField,
    Button,
    Avatar,
    Typography,
    Box,
    List,
    ListItem,
    Divider,
    Container,
} from "@mui/material";
import { useSelector } from "react-redux";
import axiosInstance from "../../utils/axiosInstance";
import { useForm } from "react-hook-form";
import { formatDistanceToNow } from "date-fns";
import { vi } from "date-fns/locale";

const CommentSection = ({ campaignId }) => {
    const [comments, setComments] = useState([]);
    const [newComment, setNewComment] = useState("");
    const [setTotalCount] = useState(0);
    const [loadedComments, setLoadedComments] = useState(5);
    const [hasMore, setHasMore] = useState(true);

    const getFirstLetter = (text) => (text ? text.trim().charAt(0) : "A");

    const fetchComments = useCallback(async () => {
        try {
            const response = await axiosInstance.get(
                `/campaign/${campaignId}/comment`,
                {
                    params: {
                        page: 1,
                        pageSize: loadedComments,
                    },
                }
            );
            setComments(
                Array.isArray(response.data.comments)
                    ? response.data.comments
                    : []
            );
            setTotalCount(response.data.totalCount);
            if (response.data.comments.length < loadedComments) {
                setHasMore(false);
            }
        } catch (error) {
            console.error("Error fetching comments:", error);
        }
    }, [campaignId, loadedComments, setTotalCount]);

    useEffect(() => {
        fetchComments();
    }, [fetchComments]);

    const appUser = useSelector((state) => state.appUser.data);

    const { register, handleSubmit } = useForm();

    const onPostComment = ({ content }) => {
        if (content.trim()) {
            const contentObj = {
                id: comments.length + 1,
                content: content,
                firstName: appUser.firstName,
                lastName: appUser.lastName,
            };
            setComments([contentObj, ...comments]);
            axiosInstance.post("/campaign-comment/create", {
                campaignId,
                content,
            });
        }
    };

    const loadMoreComments = () => {
        setLoadedComments((prevLoaded) => prevLoaded + 5);
    };

    return (
        <Container>
            <Box sx={{ width: "100%", marginTop: "4rem" }}>
                <form onSubmit={handleSubmit(onPostComment)}>
                    <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                        <Avatar sx={{ width: 40, height: 40, mr: 2 }}>
                            {getFirstLetter(appUser.firstName)}
                        </Avatar>
                        <TextField
                            fullWidth
                            label="Hãy bình luận cho chiến dịch này..."
                            variant="outlined"
                            value={newComment}
                            sx={{ marginRight: 1 }}
                            multiline
                            {...register("content", {
                                onChange: (event) =>
                                    setNewComment(event.target.value),
                            })}
                        />
                        <Button
                            variant="contained"
                            color="primary"
                            onClick={handleSubmit(onPostComment)}
                            disabled={!newComment.trim()}
                        >
                            Post
                        </Button>
                    </Box>
                </form>

                <Divider />

                <List sx={{ mt: 2 }}>
                    {comments &&
                        comments?.length === 0 &&
                        "Hãy là người đầu tiên bình luận!"}
                    {comments &&
                        comments?.length !== 0 &&
                        comments.map((comment) => (
                            <ListItem key={comment.id} sx={{ py: 1 }}>
                                <Avatar sx={{ width: 40, height: 40, mr: 2 }}>
                                    {getFirstLetter(comment.firstName)}
                                </Avatar>
                                <Box sx={{ width: "100%" }}>
                                    <Typography
                                        variant="subtitle2"
                                        sx={{
                                            fontWeight: "bold",
                                            fontSize: "1.2rem",
                                        }}
                                    >
                                        {`${comment.firstName} ${comment.lastName}`}
                                    </Typography>
                                    <Typography variant="body2" sx={{ mb: 1 }}>
                                        {`${comment.content}`}
                                    </Typography>
                                    <Typography
                                        variant="body1"
                                        sx={{ textAlign: "end" }}
                                    >
                                        {comment.createdAt
                                            ? formatDistanceToNow(
                                                  new Date(comment.createdAt),
                                                  {
                                                      addSuffix: true,
                                                      locale: vi,
                                                  }
                                              )
                                            : "bây giờ"}
                                    </Typography>
                                </Box>
                            </ListItem>
                        ))}
                </List>
                {hasMore && (
                    <Box
                        sx={{
                            display: "flex",
                            justifyContent: "center",
                            my: 5,
                        }}
                    >
                        <Button
                            variant="contained"
                            color="default"
                            sx={{
                                width: "50%",
                                py: 1,
                                backgroundColor: "grey[300]",
                                "%:hover": { backgroundColor: "grey[500]" },
                            }}
                            onClick={loadMoreComments}
                        >
                            Xem thêm ý kiến
                        </Button>
                    </Box>
                )}
            </Box>
        </Container>
    );
};

export default CommentSection;
